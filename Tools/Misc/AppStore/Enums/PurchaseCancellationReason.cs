using System.Runtime.Serialization;

namespace Tools.Misc.AppStore.Enums
{
	public enum PurchaseCancellationReason
	{
		[EnumMember(Value = "1")]
		Issue,

		[EnumMember(Value = "0")]
		Other
	}
}