using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Authentication;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Tools.Finances.Ckassa.Attributes;
using Tools.Finances.Ckassa.Requests;
using Tools.Finances.Ckassa.Responses;
using Microsoft.Extensions.Logging;

namespace Tools.Finances.Ckassa
{
	public class CkassaClient
	{
		private string ShopToken { get; }

		private string ShopSecretKey { get; }

		private string PaymentGatewayUrl { get; }

		private X509Certificate2 ClientCertificate { get; set; }

		private ILogger Logger { get; set; }

		public CkassaClient(string shopToken, string shopSecretKey, string paymentGatewayUrl, X509Certificate2 clientCertificate, ILogger logger = null)
		{
			ShopToken = shopToken;
			ShopSecretKey = shopSecretKey;
			PaymentGatewayUrl = paymentGatewayUrl.TrimEnd('/');
			ClientCertificate = clientCertificate;
			Logger = logger;
		}

		public Task<ResponseWrapper<RegisterUserResponse, Error>> RegisterUserAsync(RegisterUserRequest request)
		{
			request.Phone = request.Phone.TrimStart('+');
			return MakeRequestAsync<RegisterUserRequest, RegisterUserResponse, Error>("user/registration", request);
		}

		public Task<ResponseWrapper<GetUserInfoResponse, Error>> GetUserInfoAsync(GetUserInfoRequest request)
		{
			request.Phone = request.Phone.TrimStart('+');
			return MakeRequestAsync<GetUserInfoRequest, GetUserInfoResponse, Error>("user/status", request);
		}

		public Task<ResponseWrapper<GetCardsResponse, Error>> GetCardsAsync(GetCardsRequest request)
		{
			return MakeRequestAsync<GetCardsRequest, GetCardsResponse, Error>("get/cards", request);
		}

		public Task<ResponseWrapper<RegisterCardResponse, Error>> RegisterCardAsync(RegisterCardRequest request)
		{
			return MakeRequestAsync<RegisterCardRequest, RegisterCardResponse, Error>("card/registration", request);
		}

		public Task<ResponseWrapper<DeactivateCardResponse, Error>> DeactivateCardAsync(DeactivateCardRequest request)
		{
			return MakeRequestAsync<DeactivateCardRequest, DeactivateCardResponse, Error>("card/deactivation", request);
		}

		public Task<ResponseWrapper<CreatePaymentResponse, CreatePaymentError>> CreatePaymentAsync(CreatePaymentRequest request)
		{
			return MakeRequestAsync<CreatePaymentRequest, CreatePaymentResponse, CreatePaymentError>("do/payment", request);
		}

		public Task<ResponseWrapper<CreateAnonymousPaymentResponse, Error>> CreateAnonymousPaymentAsync(CreateAnonymousPaymentRequest request)
		{
			return MakeRequestAsync<CreateAnonymousPaymentRequest, CreateAnonymousPaymentResponse, Error>("do/payment/anonymous", request);
		}

		public Task<ResponseWrapper<GetPaymentInfoResponse, Error>> GetPaymentInfoAsync(GetPaymentInfoRequest request)
		{
			return MakeRequestAsync<GetPaymentInfoRequest, GetPaymentInfoResponse, Error>("check/payment/state", request);
		}

		public Task<ResponseWrapper<ConfirmPaymentResponse, Error>> ConfirmPaymentAsync(ConfirmPaymentRequest request)
		{
			return MakeRequestAsync<ConfirmPaymentRequest, ConfirmPaymentResponse, Error>("provision-services/confirm", request);
		}

		public Task<ResponseWrapper<RefundPaymentResponse, Error>> RefundPaymentAsync(RefundPaymentRequest request)
		{
			return MakeRequestAsync<RefundPaymentRequest, RefundPaymentResponse, Error>("provision-services/refund", request);
		}

		private string GetAbsoluteActionAddress(string actionRelativeAddress)
		{
			return $"{PaymentGatewayUrl}/{actionRelativeAddress}";
		}

		private async Task<ResponseWrapper<TResponse, TError>> MakeRequestAsync<TRequest, TResponse, TError>(string actionRelativeAddress, TRequest request) where TError : Error, new()
		{
			var serializer = GetDefaultSerializer();
			var requestJObject = JObject.FromObject(request, serializer);
			requestJObject["shopToken"] = JToken.FromObject(ShopToken);
			requestJObject["sign"] = GenerateMessageSignature(request, true);
			var requestString = requestJObject.ToString(Formatting.None);
			var handler = new HttpClientHandler
			{	
				ClientCertificates = { ClientCertificate },
				SslProtocols = SslProtocols.Tls12
			};
			using (var client = new HttpClient(handler))
			{
				var actionAddress = GetAbsoluteActionAddress(actionRelativeAddress);
				Logger?.LogInformation("Ckassa action url: " + actionAddress);
				Logger?.LogInformation("Ckassa request: " + requestString);
				var httpResponse = await client.PostAsync(actionAddress, 
					new StringContent(requestString, Encoding.UTF8, "application/json"));
				var responseString = await httpResponse.Content.ReadAsStringAsync();
				Logger?.LogInformation("Ckassa response: " + responseString);
				var result = new ResponseWrapper<TResponse, TError>();
				using (var reader = new JsonTextReader(new StringReader(responseString)))
				{
					var responseJObject = await JToken.ReadFromAsync(reader);
					var response = responseJObject.ToObject<TResponse>();
					if (httpResponse.StatusCode == HttpStatusCode.OK)
					{
						var serverSignature = responseJObject["sign"];
						var generatedSignature = GenerateMessageSignature(response, false);
						if (StringComparer.Ordinal.Compare(serverSignature, generatedSignature) != 0)
						{
							result.Error = new TError
							{
								Message = "Incorrect server signature",
								UserMessage = "Подпись сервера не прошла валидацию"
							};
						}
						else
						{
							result.Data = response;
						}
					}
					else
					{
						result.Error = responseJObject.ToObject<TError>();
					}
					return result;
				}
			}			
		}

		private string GenerateMessageSignature<TMessage>(TMessage message, bool includeShopToken)
		{
			var serializer = GetDefaultSerializer();
			var jObj = JObject.FromObject(message, serializer);
			var elements = GetSignatureElements(typeof(TMessage), jObj, serializer);
			if (includeShopToken)
			{
				elements.Add(ShopToken);
			}
			elements.Add(ShopSecretKey);
			var inputString = string.Join("&", elements);
			return ComputeHash(ComputeHash(inputString).ToUpperInvariant()).ToUpperInvariant();
		}

		private List<string> GetSignatureElements(Type type, JToken token, JsonSerializer serializer)
		{
			var result = new List<string>();
			switch (token.Type)
			{
				case JTokenType.Object:	
					var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
						.Where(property => property.IsDefined(typeof(SignOrderAttribute)))
						.OrderBy(property => (property.GetCustomAttribute(typeof(SignOrderAttribute)) as SignOrderAttribute).Order);
					foreach (var property in properties)
					{
						var serializedName = property.GetCustomAttributes(typeof(JsonPropertyAttribute)).Cast<JsonPropertyAttribute>().FirstOrDefault()?.PropertyName ??
						                     (serializer.ContractResolver as DefaultContractResolver)?.GetResolvedPropertyName(property.Name);
						if (serializedName == null)
						{
							continue;
						}
						var currentToken = token[serializedName];
						if (currentToken != null)
						{
							result.AddRange(GetSignatureElements(property.PropertyType, currentToken,
								serializer));
						}
					}
					break;
				case JTokenType.Array:
					var jArray = token as JArray;
					var elementsType = type.IsArray ? type.GetElementType() : type.GetGenericArguments().FirstOrDefault();
					if (elementsType != null && jArray != null)
					{
						foreach (var arrayElement in jArray)
						{
							result.AddRange(GetSignatureElements(elementsType, arrayElement, serializer));
						}
						return result;
					}
					break;
				case JTokenType.Date:
					result.Add(token.ToObject<DateTime>().ToString(serializer.DateFormatString));
					break;
				case JTokenType.Boolean:
					result.Add(token.Value<string>().ToLower());
					break;
				default:
					result.Add(token.Value<string>());
					break;
			}
			return result;
		}

		private JsonSerializer GetDefaultSerializer()
		{
			var result = new JsonSerializer
			{
				ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new CamelCaseNamingStrategy()
				},
				NullValueHandling = NullValueHandling.Ignore,
				DateFormatString = "yyyy-MM-dd HH:mm:ss"
			};
			result.Converters.Add(new StringEnumConverter
			{
				NamingStrategy = new CamelCaseNamingStrategy()
			});
			return result;
		}

		private string ComputeHash(string s)
		{
			return string.Concat(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(s))
				.Select(item => item.ToString("x2")));
		}
	}
}
