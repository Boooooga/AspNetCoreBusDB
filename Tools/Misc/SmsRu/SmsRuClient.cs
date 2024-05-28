using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Tools.Misc.SmsRu.Requests;
using Tools.Misc.SmsRu.Responses;
using Tools.Serialization;
using Tools.Serialization.Converters;
using UnixDateTimeConverter = Tools.Serialization.Converters.UnixDateTimeConverter;

namespace Tools.Misc.SmsRu
{
	public class SmsRuClient
	{
		public string ApiKey { get; }

		private string ApiUrl => "https://sms.ru";

		public SmsRuClient(string apiKey)
		{
			ApiKey = apiKey;
		}

		public Task<SendSmsResponse> SendSmsAsync(SendSmsRequest request)
		{
			return MakeRequestAsync<SendSmsRequest, SendSmsResponse>("sms/send", request);
		}

		public Task<CreateCallCheckResponse> CreateCallCheckAsync(CreateCallCheckRequest request)
		{
			return MakeRequestAsync<CreateCallCheckRequest, CreateCallCheckResponse>("callcheck/add", request);
		}

		public Task<GetCallCheckStatusResponse> GetCallCheckStatusAsync(GetCallCheckStatusRequest request)
		{
			return MakeRequestAsync<GetCallCheckStatusRequest, GetCallCheckStatusResponse>("callcheck/status", request);
		}

		private async Task<TResponse> MakeRequestAsync<TRequest, TResponse>(string relativeActionAddress, TRequest request)
		{
			using var client = new HttpClient();
			var jObject = JObject.FromObject(request, CreateSerializer());
			var requestData = new Dictionary<string, string>
			{
				["json"] = "1",
				["api_id"] = ApiKey
			};
			BuildFormData(jObject, "", requestData);
			var requestContent = new FormUrlEncodedContent(requestData);
			var requestText = await requestContent.ReadAsStringAsync();
			var response = await client.PostAsync(GetAbsoluteActionAddress(relativeActionAddress), requestContent);
			var responseText = await response.Content.ReadAsStringAsync();
			using var reader = new JsonTextReader(new StringReader(responseText));
			return CreateSerializer().Deserialize<TResponse>(reader);
		}

		private string GetAbsoluteActionAddress(string relativeActionAddress)
		{
			return $"{ApiUrl.TrimEnd('/')}/{relativeActionAddress.TrimStart('/')}";
		}

		private void BuildFormData(JObject jObject, string prefix, Dictionary<string, string> result)
		{
			foreach (var property in jObject.Properties())
			{
				var fullPropertyName = string.IsNullOrEmpty(prefix) ? property.Name : prefix + "[" + property.Name + "]";
				switch (property.Value.Type)
				{
					case JTokenType.Object:
						BuildFormData((JObject)property.Value, fullPropertyName, result);
						break;
					default:
						result[fullPropertyName] = property.Value.Value<string>();
						break;
				}
			}
		}

		private JsonSerializer CreateSerializer()
		{
			var result = new JsonSerializer
			{
				ContractResolver = new DefaultContractResolver
				{
					NamingStrategy = new SnakeCaseNamingStrategy()
				},
				NullValueHandling = NullValueHandling.Ignore
			};
			result.Converters.Add(new UnixDateTimeConverter(UnixDateTimeConverter.SerializedValueUnit.Seconds));
			result.Converters.Add(new BooleanConverter(1, 0));
			return result;
		}
	}
}
