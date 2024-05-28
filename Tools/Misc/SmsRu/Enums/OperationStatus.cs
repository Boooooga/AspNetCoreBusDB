using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tools.Misc.SmsRu.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OperationStatus
	{
		Ok,
		Error
	}
}