using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tools.Misc.SmsRu.Enums;

namespace Tools.Misc.SmsRu.Responses
{
	public class BaseResponse
	{
		public OperationStatus Status { get; set; }

		[JsonProperty(PropertyName = "status_code")]
		public int StatusCode { get; set; }
	}
}
