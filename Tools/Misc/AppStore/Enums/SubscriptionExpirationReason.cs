using System.Runtime.Serialization;

namespace Tools.Misc.AppStore.Enums
{
	public enum SubscriptionExpirationReason
	{
		[EnumMember(Value = "1")]
		CancelledByUser,

		[EnumMember(Value = "2")]
		BillingError,

		[EnumMember(Value = "3")]
		PriceChangeDeclined,

		[EnumMember(Value = "4")]
		ProductNotAvailable,

		[EnumMember(Value = "5")]
		Unknown,
	}
}