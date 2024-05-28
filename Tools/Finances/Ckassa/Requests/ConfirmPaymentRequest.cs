using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tools.Finances.Ckassa.Attributes;

namespace Tools.Finances.Ckassa.Requests
{
	/// <summary>
	/// Запрос на подтверждение замороженного платежа.
	/// </summary>
	public class ConfirmPaymentRequest
	{
		/// <summary>
		/// Идентификатор платежа.
		/// </summary>
		[SignOrder(0)]
		[JsonProperty("regPayNum")]
		public string PaymentId { get; set; }

		/// <summary>
		/// Уникальный идентификатор заказа на стороне магазина.
		/// </summary>
		[SignOrder(1)]
		public string OrderId { get; set; }

		/// <summary>
		/// Подтверждаемая сумма платежа (в копейках).
		/// </summary>
		[SignOrder(2)]
		public long? Amount { get; set; }

		public ConfirmPaymentRequest(string paymentId, string orderId)
		{
			PaymentId = paymentId;
			OrderId = orderId;
		}

		public ConfirmPaymentRequest(string paymentId, string orderId, long? amount)
		{
			PaymentId = paymentId;
			OrderId = orderId;
			Amount = amount;
		}
	}
}
