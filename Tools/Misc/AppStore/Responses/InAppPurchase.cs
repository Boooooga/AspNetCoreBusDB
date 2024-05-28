using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Tools.Misc.AppStore.Enums;
using Tools.Serialization;
using Tools.Serialization.Converters;

namespace Tools.Misc.AppStore.Responses
{
	public class InAppPurchase
	{
		[JsonProperty(PropertyName = "cancellation_date_ms")]
		public DateTime? CancellationDate { get; set; }

		public PurchaseCancellationReason? CancellationReason { get; set; }

		[JsonProperty(PropertyName = "expires_date_ms")]
		public DateTime? ExpirationDate { get; set; }

		[JsonProperty(PropertyName = "is_in_intro_offer_period")]
		[JsonConverter(typeof(BooleanConverter), "true", "false")]
		public bool? IsIntroductoryPricePeriodActive { get; set; }

		[JsonProperty(PropertyName = "is_trial_period")]
		[JsonConverter(typeof(BooleanConverter), "true", "false")]
		public bool? IsTrialPeriodActive { get; set; }

		[JsonProperty(PropertyName = "is_upgraded")]
		[JsonConverter(typeof(BooleanConverter), "true", "false")]
		public bool? IsUpgraded { get; set; }

		[JsonProperty(PropertyName = "original_purchase_date_ms")]
		public DateTime OriginalPurchaseDate { get; set; }

		public string OriginalTransactionId { get; set; }

		public string ProductId { get; set; }

		public string PromotionalOfferId { get; set; }

		[JsonProperty(PropertyName = "purchase_date_ms")]
		public DateTime PurchaseDate { get; set; }

		public int Quantity { get; set; }

		public string SubscriptionGroupIdentifier { get; set; }

		public string TransactionId { get; set; }

		[JsonProperty(PropertyName = "web_order_line_item_id")]
		public string PurchaseEventId { get; set; }
	}
}
