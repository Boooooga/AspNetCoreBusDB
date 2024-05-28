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
	/// Запрос получения информации о платеже.
	/// </summary>
	public class GetPaymentInfoRequest
	{
		/// <summary>
		/// Идентификатор платежа.
		/// </summary>
		[SignOrder(0)]
		[JsonProperty("regPayNum")]
		public string PaymentId { get; set; }

		public GetPaymentInfoRequest(string paymentId)
		{
			PaymentId = paymentId;
		}
	}
}
