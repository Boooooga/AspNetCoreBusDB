using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Tools.Misc.SmsRu.Responses
{
	public class CreateCallCheckResponse : BaseResponse
	{
		public string CheckId { get; set; }

		public string CallPhone { get; set; }

		[JsonProperty(PropertyName = "call_phone_pretty")]
		public string CallPhoneFormatted { get; set; }

		public string CallPhoneHtml { get; set; }
	}
}
