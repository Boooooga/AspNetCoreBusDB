using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Tools.Misc.SmsRu.Requests
{
	public class SendSmsRequest
	{
		[JsonProperty(PropertyName = "multi")]
		public Dictionary<string, string> Messages { get; set; }

		[JsonProperty(PropertyName = "from")]
		public string SenderName { get; set; }

		[JsonProperty(PropertyName = "time")]
		public DateTime? SendTime { get; set; }

		public int? Ttl { get; set; }

		[JsonProperty(PropertyName = "daytime")]
		public bool SendAtDayTime { get; set; }

		[JsonProperty(PropertyName = "translit")]
		public bool EnableTransliteration { get; set; }

		[JsonProperty(PropertyName = "partner_id")]
		public int? PartnerId { get; set; }

		public SendSmsRequest(string phone, string messageText)
		{
			Messages = new Dictionary<string, string>
			{
				[phone] = messageText
			};
		}

		public SendSmsRequest(Dictionary<string, string> messages)
		{
			Messages = messages;
		}
	}
}
