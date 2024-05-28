using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Tools.Finances.Ckassa.Attributes;
using Tools.Finances.Ckassa.Enums;
using Tools.Finances.Ckassa.Models;

namespace Tools.Finances.Ckassa.Requests
{
	/// <summary>
	/// Запрос на создание анонимного платежа.
	/// </summary>
	public class CreateAnonymousPaymentRequest
	{
		/// <summary>
		/// Код провайдера услуг.
		/// </summary>
		[SignOrder(0)]
		[JsonProperty("serviceCode")]
		public string ProviderCode { get; set; }

		/// <summary>
		/// Сумма платежа (в копейках).
		/// </summary>
		[SignOrder(1)]
		public long Amount { get; set; }

		/// <summary>
		/// Комиссия за платеж (в копейках).
		/// </summary>
		[SignOrder(2)]
		[JsonProperty("comission")]
		public long Commission { get; set; }

		/// <summary>
		/// Способ оплаты.
		/// </summary>
		[SignOrder(3)]
		[JsonProperty("payType")]
		public PaymentSource Source { get; set; }

		/// <summary>
		/// Тип формы оплаты.
		/// </summary>
		[SignOrder(4)]
		[JsonProperty("clientType")]
		public PaymentFormType FormType { get; set; }


		/// <summary>
		/// Время истечения срока жизни заказа в секундах.
		/// </summary>
		[SignOrder(5)]
		[JsonProperty("orderBestBefore")]
		public long OrderBestBefore { get; set; }

		/// <summary>
		/// Параметры (реквизиты) платежа.
		/// </summary>
		[SignOrder(6)]
		[JsonProperty("properties")]
		public List<PaymentDetail> Details { get; set; }

		public CreateAnonymousPaymentRequest(string providerCode, long amount, long commission)
		{
			ProviderCode = providerCode;
			Amount = amount;
			Commission = commission;
		}
	}
}
