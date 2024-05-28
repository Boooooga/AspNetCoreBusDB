using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Tools.Finances.Sberbank.Enums;
using Tools.Finances.Sberbank.Responses.Models;

namespace Tools.Finances.Sberbank.Responses
{
	/// <summary>
	/// Класс, описывающий ответ на запрос создания нового заказа с оплатой через Apple Pay (<see cref="Requests.RegisterApplePayOrderRequest"/>).
	/// </summary>
	/// <inheritdoc />
	public class RegisterApplePayOrderResponse : MobilePaymentBaseResponse
	{
		/// <summary>
		/// Информация о заказе.
		/// </summary>
		[JsonProperty("OrderStatus")]
		public ApplePayOrderInfo OrderInfo { get; set; }
	}
}
