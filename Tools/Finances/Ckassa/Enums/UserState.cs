using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Tools.Finances.Ckassa.Enums
{
	/// <summary>
	/// Статус пользователя.
	/// </summary>
	[JsonConverter(typeof(StringEnumConverter))]
	public enum UserState
	{
		/// <summary>
		/// Операции доступны.
		/// </summary>
		[EnumMember(Value = "active")]
		Active,

		/// <summary>
		/// Операции недоступны, требуется активация.
		/// </summary>
		[EnumMember(Value = "inactive")]
		Inactive,

		/// <summary>
		/// Пользователь заблокирован (операции недоступны, активация невозможна).
		/// </summary>
		[EnumMember(Value = "disable")]
		Disabled
	}
}