using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Tools.Serialization.Converters
{
	public class UnixDateTimeConverter : DateTimeConverterBase
	{
		private static readonly DateTime UnixEpochStart = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
		private readonly TimeZoneInfo localTimeZoneInfo;
		private readonly SerializedValueUnit unit;
		private readonly SerializedValueType serializedValueType;

		public UnixDateTimeConverter(SerializedValueUnit unit = SerializedValueUnit.Milliseconds, SerializedValueType serializedValueType = SerializedValueType.Integer, TimeZoneInfo localTimeZoneInfo = null)
		{
			this.localTimeZoneInfo = localTimeZoneInfo ?? TimeZoneInfo.Local;
			this.unit = unit;
			this.serializedValueType = serializedValueType;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteValue((object)null);
				return;
			}
			var date = (DateTime)value;
			var span = TimeZoneInfo.ConvertTimeToUtc(date, localTimeZoneInfo) - UnixEpochStart;
			var integerValue = (long)(unit == SerializedValueUnit.Milliseconds ? span.TotalMilliseconds : span.TotalSeconds);
			if (serializedValueType == SerializedValueType.Integer)
			{
				writer.WriteValue(integerValue);
			}
			else
			{
				writer.WriteValue(integerValue.ToString());
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.Value == null)
				return null;
			long? integerValue = null;
			switch (reader.TokenType)
			{
				case JsonToken.Integer:
					integerValue = (long)reader.Value;
					break;
				case JsonToken.String:
				{
					if (long.TryParse((string)reader.Value, out var parseResult))
					{
						integerValue = parseResult;
					}
					break;
				}
			}
			if (integerValue == null)
			{
				throw new JsonSerializationException($"Can not convert value {reader.Value} to type {objectType}");
			}
			return TimeZoneInfo.ConvertTimeFromUtc(unit == SerializedValueUnit.Milliseconds ? UnixEpochStart.AddMilliseconds(integerValue.Value) :
				UnixEpochStart.AddSeconds(integerValue.Value), localTimeZoneInfo);
		}

		public enum SerializedValueUnit
		{
			Milliseconds,
			Seconds
		}

		public enum SerializedValueType
		{
			Integer,
			String
		}
	}
}
