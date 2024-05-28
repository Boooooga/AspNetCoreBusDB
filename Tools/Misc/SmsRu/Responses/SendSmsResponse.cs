using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tools.Misc.SmsRu.Enums;

namespace Tools.Misc.SmsRu.Responses
{
	public class SendSmsResponse : BaseResponse
	{
		[JsonProperty(PropertyName = "sms")]
		public Dictionary<string, SmsDetails> Details { get; set; }

		public decimal Balance { get; set; }

		public class SmsDetails
		{
			public OperationStatus Status { get; set; }

			public int StatusCode { get; set; }

			[JsonProperty(PropertyName = "status_text")]
			public string StatusComment { get; set; }

			public string SmsId { get; set; }

			public decimal? Cost { get; set; }
		} 
	}
}
