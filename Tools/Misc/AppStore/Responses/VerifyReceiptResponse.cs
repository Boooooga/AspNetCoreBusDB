using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Tools.Misc.AppStore.Enums;
using Tools.Serialization;
using Tools.Serialization.Converters;

namespace Tools.Misc.AppStore.Responses
{
	public class VerifyReceiptResponse
	{
		public Enums.StoreEnvironment Environment { get; set; }

		[JsonProperty(PropertyName = "is-retryable")]
		[JsonConverter(typeof(BooleanConverter), 1, 0)]
		public bool? IsRetryable { get; set; }

		public Receipt Receipt { get; set; }

		[JsonProperty(PropertyName = "pending_renewal_info")]
		public List<SubscriptionRenewal> Renewals { get; set; }

		[JsonProperty(PropertyName = "latest_receipt_info")]
		public List<InAppPurchase> LatestPurchases { get; set; }

		public ReceiptStatus Status { get; set; }
	}
}
