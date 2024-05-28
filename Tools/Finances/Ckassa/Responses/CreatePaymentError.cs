using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Tools.Finances.Ckassa.Attributes;

namespace Tools.Finances.Ckassa.Responses
{
	public class CreatePaymentError : Error
	{
		[JsonProperty("regPayNum")]
		public string PaymentId { get; set; }

		[JsonProperty("securePageURL")]
		public string PaymentUrl { get; set; }
	}
}
