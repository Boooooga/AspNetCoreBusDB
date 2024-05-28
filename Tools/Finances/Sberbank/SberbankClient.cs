using System;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Tools.Finances.Sberbank.Enums;
using Tools.Finances.Sberbank.Requests;
using Tools.Finances.Sberbank.Responses;

namespace Tools.Finances.Sberbank
{
	public class SberbankClient
	{
		/// <summary>
		/// Имя пользователя для подключения к платежному шлюзу.
		/// </summary>
		private string UserName { get; }

		/// <summary>
		/// Пароль пользователя для подключения к платежному шлюзу.
		/// </summary>
		private string Password { get; }

		/// <summary>
		/// Адрес платежного шлюза
		/// </summary>
		private string PaymentGatewayUrl { get; }

		/// <summary>
		/// Инициализирует новый класс клиента для взаимодействия с платежным шлюзом Сбербанка через REST-интерфейс.
		/// </summary>
		/// <param name="userName">Имя пользователя для подключения к платежному шлюзу.</param>
		/// <param name="password">Пароль пользователя для подключения к платежному шлюзу.</param>
		/// <param name="paymentGatewayUrl">Адрес платежного шлюза.</param>
		public SberbankClient(string userName, string password, string paymentGatewayUrl)
		{
			UserName = userName;
			Password = password;
			PaymentGatewayUrl = paymentGatewayUrl.TrimEnd('/');
		}

		/// <summary>
		/// Создает новый заказ.
		/// </summary>
		/// <param name="request">Объект, содержащий данные запроса.</param>
		public Task<RegisterOrderResponse> RegisterOrderAsync(RegisterOrderRequest request)
		{
			return MakeRequestAsync<RegisterOrderRequest, RegisterOrderResponse>("rest/register.do", request);
		}

		/// <summary>
		/// Создает новый заказ, оплачиваемый посредством системы Apple Pay.
		/// </summary>
		/// <param name="request">Объект, содержащий данные запроса.</param>
		public Task<RegisterApplePayOrderResponse> RegisterApplePayOrderAsync(RegisterApplePayOrderRequest request)
		{
			return MakeRequestAsync<RegisterApplePayOrderRequest, RegisterApplePayOrderResponse>("applepay/payment.do", request);
		}

		/// <summary>
		/// Завершает оплату ранее предавторизованного заказа.
		/// </summary>
		/// <param name="request">Объект, содержащий данные запроса.</param>
		public Task<CompleteOrderPaymentResponse> CompleteOrderPaymentAsync(CompleteOrderPaymentRequest request)
		{
			return MakeRequestAsync<CompleteOrderPaymentRequest, CompleteOrderPaymentResponse>("rest/deposit.do", request);
		}

		/// <summary>
		/// Отменяет оплату ранее предавторизованного заказа.
		/// </summary>
		/// <param name="request">Объект, содержащий данные запроса.</param>
		/// <returns>Объект, содержащий ответ платежного шлюза.</returns>
		public Task<CancelOrderPaymentResponse> CancelOrderPaymentAsync(CancelOrderPaymentRequest request)
		{
			return MakeRequestAsync<CancelOrderPaymentRequest, CancelOrderPaymentResponse>("rest/reverse.do", request);
		}

		/// <summary>
		/// Запрашивает информацию о заказе.
		/// </summary>
		/// <param name="request">Объект, содержащий данные запроса.</param>
		public Task<GetOrderStatusResponse> GetOrderStatusAsync(GetOrderStatusRequest request)
		{
			return MakeRequestAsync<GetOrderStatusRequest, GetOrderStatusResponse>("rest/getOrderStatus.do", request);
		}

		/// <summary>
		/// Возвращает полный адрес REST-запроса для заданного действия.
		/// </summary>
		/// <param name="actionRelativeAddress">Относительный адрес действия.</param>
		private string GetAbsoluteActionAddress(string actionRelativeAddress)
		{
			return string.Format("{0}/payment/{1}", PaymentGatewayUrl, actionRelativeAddress);
		}

		/// <summary>
		/// Направляет платежному шлюзу запрос на осуществление действия с заданным относительным адресом.
		/// </summary>
		/// <typeparam name="TRequest">Тип, описывающий запрос.</typeparam>
		/// <typeparam name="TResponse">Тип, описывающий ответ шлюза.</typeparam>
		/// <param name="actionRelativeAddress">Относительный адрес выполняемого действия.</param>
		/// <param name="request">Данные запроса.</param>
		private async Task<TResponse> MakeRequestAsync<TRequest, TResponse>(string actionRelativeAddress, TRequest request) 
			where TRequest: BaseRequest where TResponse : BaseResponse
		{
			var serializer = request.CreateSerializer();
			var requestString = serializer.Serialize(request, UserName, Password);
			string contentType;
			switch (serializer.SerializationFormat)
			{
				case RequestSerializationFormat.WebForm:
					contentType = "application/x-www-form-urlencoded";
					break;
				case RequestSerializationFormat.Json:
					contentType = "application/json";
					break;
				default:
					throw new NotSupportedException("Формат сериализации не поддерживается");
			}
			var handler = new HttpClientHandler
			{	
				SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12,
			};
			using (var client = new HttpClient(handler))
			{
				var response = await client.PostAsync(GetAbsoluteActionAddress(actionRelativeAddress), 
					new StringContent(requestString, Encoding.UTF8, contentType));
				var responseContent = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<TResponse>(responseContent);
			}			
		}
	}
}
