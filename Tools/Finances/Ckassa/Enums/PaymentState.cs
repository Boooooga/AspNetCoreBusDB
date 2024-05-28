using System.Runtime.Serialization;

namespace Tools.Finances.Ckassa.Enums
{
	/// <summary>
	/// Статус платежа.
	/// </summary>
	public enum PaymentState
	{
		[EnumMember(Value = "created_error")]
		CreatingError,

		[EnumMember(Value = "created")]
		Created,

		[EnumMember(Value = "rejected")]
		Rejected,

		[EnumMember(Value = "refunded")]
		Refunded,

		[EnumMember(Value = "payed")]
		Payed,

		[EnumMember(Value = "holded")]
		Hold,

		[EnumMember(Value = "processed")]
		Processed,

		[EnumMember(Value = "error")]
		ProcessingError,
	}
}