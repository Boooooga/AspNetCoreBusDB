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
	/// Запрос на отмену замороженного платежа.
	/// </summary>
	public class RefundPaymentRequest
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

		public RefundPaymentRequest(string paymentId, string orderId)
		{
			PaymentId = paymentId;
			OrderId = orderId;
		}
	}
}
