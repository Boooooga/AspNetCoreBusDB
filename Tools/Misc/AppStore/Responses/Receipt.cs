using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tools.Misc.AppStore.Enums;

namespace Tools.Misc.AppStore.Responses
{
	public class Receipt
	{
		public long AdamId { get; set; }

		public long AppItemId { get; set; }

		public string ApplicationVersion { get; set; }

		public string BundleId { get; set; }

		public long DownloadId { get; set; }

		[JsonProperty(PropertyName = "expiration_date_ms")]
		public DateTime? ExpirationDate { get; set; }

		public string OriginalApplicationVersion { get; set; }

		[JsonProperty(PropertyName = "original_purchase_date_ms")]
		public DateTime OriginalPurchaseDate { get; set; }

		[JsonProperty(PropertyName = "preorder_date_ms")]
		public DateTime? PreOrderDate { get; set; }

		[JsonProperty(PropertyName = "in_app")]
		public List<InAppPurchase> Purchases { get; set; }

		[JsonProperty(PropertyName = "receipt_creation_date_ms")]
		public DateTime ReceiptCreationDate { get; set; }

		public ReceiptType ReceiptType { get; set; }

		[JsonProperty(PropertyName = "request_date_ms")]
		public DateTime RequestDate { get; set; }
		
		public long VersionExternalIdentifier { get; set; }
	}
}
