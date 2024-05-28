using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Tools.Misc.AppStore.Requests
{
	public class VerifyReceiptRequest
	{
		[JsonProperty(PropertyName = "receipt-data")]
		public IList<byte> Receipt;

		[JsonProperty(PropertyName = "password")]
		public string Password { get; set; }

		[JsonProperty(PropertyName = "exclude-old-transactions")]
		public bool? ExcludeOldTransactions { get; set; }

		public VerifyReceiptRequest(IList<byte> receipt, string password = null)
		{
			Receipt = receipt;
			Password = password;
		}
	}
}
