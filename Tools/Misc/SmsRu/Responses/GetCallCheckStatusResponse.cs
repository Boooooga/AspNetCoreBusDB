using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tools.Misc.SmsRu.Enums;

namespace Tools.Misc.SmsRu.Responses
{
	public class GetCallCheckStatusResponse : BaseResponse
	{
		public CallCheckStatus CheckStatus { get; set; }

		[JsonProperty(PropertyName = "check_status_text")]
		public string CheckStatusComment { get; set; }
	}
}
