using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tools.Finances.Ckassa.Attributes;
using Tools.Finances.Ckassa.Enums;
using Tools.Finances.Ckassa.Models;

namespace Tools.Finances.Ckassa.Requests
{
	/// <summary>
	/// Запрос на создание платежа.
	/// </summary>
	public class CreatePaymentRequest
	{
		/// <summary>
		/// Код провайдера услуг.
		/// </summary>
		[SignOrder(0)]
		[JsonProperty("serviceCode")]
		public string ProviderCode { get; set; }

		/// <summary>
		/// Идентификатор пользователя (плательщика).
		/// </summary>
		[SignOrder(1)]
		public string UserToken { get; set; }

		/// <summary>
		/// Сумма платежа (в копейках).
		/// </summary>
		[SignOrder(2)]
		public long Amount { get; set; }

		/// <summary>
		/// Комиссия за платеж (в копейках).
		/// </summary>
		[SignOrder(3)]
		[JsonProperty("comission")]
		public long Commission { get; set; }

		/// <summary>
		/// Идентификатор карты, с которой производится рекуррентный платеж.
		/// </summary>
		[SignOrder(4)]
		public string CardToken { get; set; }

		/// <summary>
		/// Токен для оплаты через Google Pay.
		/// </summary>
		[SignOrder(5)]
		[JsonProperty("gpayToken")]
		public string GooglePayToken { get; set; }

		/// <summary>
		/// Токен для оплаты через Apple Pay.
		/// </summary>
		[SignOrder(6)]
		public string ApplePayToken { get; set; }

		/// <summary>
		/// Возможно участие пользователя в проводке рекуррентного платежа.
		/// </summary>
		[SignOrder(7)]
		[JsonProperty("enableSMSConfirm")]
		public bool SmsConfirmationAllowed { get; set; }

		/// <summary>
		/// Способ оплаты.
		/// </summary>
		[SignOrder(8)]
		[JsonProperty("payType")]
		public PaymentSource Source { get; set; }

		/// <summary>
		/// Тип формы оплаты.
		/// </summary>
		[SignOrder(9)]
		[JsonProperty("clientType")]
		public PaymentFormType FormType { get; set; }

		/// <summary>
		/// Номер телефона плательщика (для платежей с баланса мобильного телефона).
		/// </summary>
		[SignOrder(10)]
		public string UserPhone { get; set; }

		/// <summary>
		/// E-Mail плательщика для отправки чека.
		/// </summary>
		[SignOrder(11)]
		public string UserEmail { get; set; }

		/// <summary>
		/// Способ отправки чека плательщику.
		/// </summary>
		[SignOrder(12)]
		[JsonProperty("fiscalType")]
		public ReceiptSendMethod ReceiptSendMethod { get; set; }

		/// <summary>
		/// Время заморозки денежных средств (если не указано, платеж проходит без заморозки).
		/// </summary>
		[SignOrder(13)]
		[JsonProperty("holdTtl")]
		public int? HoldTime { get; set; }

		/// <summary>
		/// Параметры (реквизиты) платежа.
		/// </summary>
		[SignOrder(14)]
		[JsonProperty("properties")]
		public List<PaymentDetail> Details { get; set; }

		public CreatePaymentRequest(string providerCode, string userToken, long amount, long commission)
		{
			ProviderCode = providerCode;
			UserToken = userToken;
			Amount = amount;
			Commission = commission;
		}
	}
}
