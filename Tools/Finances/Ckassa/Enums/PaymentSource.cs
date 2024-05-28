using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tools.Finances.Ckassa.Enums
{
	/// <summary>
	/// Способ оплаты.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PaymentSource
	{
		[EnumMember(Value = "card")]
		Card,

		[EnumMember(Value = "gPay")]
		GooglePay,

		[EnumMember(Value = "applePay")]
		ApplePay
	}
}