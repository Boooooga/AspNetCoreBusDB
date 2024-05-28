using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Tools.Misc.AppStore.Enums;
using Tools.Serialization;
using Tools.Serialization.Converters;

namespace Tools.Misc.AppStore.Responses
{
	public class SubscriptionRenewal
	{
		[JsonProperty(PropertyName = "auto_renew_product_id")]
		public string RenewingProductId { get; set; }

		[JsonProperty(PropertyName = "auto_renew_status")]
		[JsonConverter(typeof(BooleanConverter), "1", "0")]
		public bool IsAutomaticRenewalEnabled { get; set; }

		[JsonProperty(PropertyName = "expiration_intent")]
		public SubscriptionExpirationReason ExpirationReason { get; set; }

		[JsonProperty(PropertyName = "grace_period_expires_date_ms")]
		public DateTime? GracePeriodExpirationDate { get; set; }

		[JsonProperty(PropertyName = "is_in_billing_retry_period")]
		[JsonConverter(typeof(BooleanConverter), "1", "0")]
		public bool? IsBillingRetryPeriodActive { get; set; }

		[JsonProperty(PropertyName = "price_consent_status")]
		[JsonConverter(typeof(BooleanConverter), "1", "0")]
		public bool? IsPriceChangeConfirmed { get; set; }

		public string OriginalTransactionId { get; set; }

		public string ProductId { get; set; }
	}
}
