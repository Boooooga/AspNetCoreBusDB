using System;
using Newtonsoft.Json;

namespace Tools.Serialization.Converters
{
	public class StringDecimalConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			writer.WriteValue(value.ToString());
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return reader.TokenType switch
			{
				JsonToken.Null when objectType == typeof(decimal) => throw new JsonSerializationException($"Can not convert null to type {objectType}"),
				JsonToken.Null => null,
				JsonToken.String => decimal.Parse(reader.ReadAsString()),
				_ => throw new JsonSerializationException($"Can not read value ${reader.Value} as {objectType}"),
			};
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(decimal) || Nullable.GetUnderlyingType(objectType) == typeof(decimal?);
		}
	}
}
