using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tools.Finances.Ckassa.Enums
{
	/// <summary>
	/// Платежная система.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum PaymentSystem
	{
		[EnumMember(Value = "undefined")]
		Undefined,

		[EnumMember(Value = "visa")]
		Visa,

		[EnumMember(Value = "master_card")]
		MasterCard,

		[EnumMember(Value = "maestro")]
		Maestro,

		[EnumMember(Value = "mir")]
		Mir
	}
}