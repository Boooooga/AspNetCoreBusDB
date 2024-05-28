using System;
using Newtonsoft.Json;

namespace Tools.Serialization.Converters
{
	public class BooleanConverter : JsonConverter
	{
		private readonly long? integerTrueValue, integerFalseValue;
		private readonly string stringTrueValue, stringFalseValue;

		public BooleanConverter(long trueValue, long falseValue)
		{
			integerTrueValue = trueValue;
			integerFalseValue = falseValue;
			if (trueValue == falseValue)
			{
				throw new ArgumentException("Arguments should not be equal");
			}
		}

		public BooleanConverter(string trueValue, string falseValue)
		{
			stringTrueValue = trueValue ?? throw new ArgumentException("Argument should not be null", nameof(trueValue));
			stringFalseValue = falseValue ?? throw new ArgumentException("Argument should not be null", nameof(falseValue));
			if (trueValue == falseValue)
			{
				throw new ArgumentException("Arguments should not be equal");
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteNull();
				return;
			}
			var boolValue = (bool)value;
			if (integerTrueValue != null)
			{
				writer.WriteValue(boolValue ? integerTrueValue.Value : integerFalseValue.Value);
			}
			else
			{
				writer.WriteValue(boolValue ? stringTrueValue : stringFalseValue);
			}
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			switch (reader.TokenType)
			{
				case JsonToken.Null when objectType == typeof(bool):
					throw new JsonSerializationException($"Can not convert null to type {objectType}");
				case JsonToken.Null:
					return null;
				case JsonToken.Integer when reader.Value.Equals(integerTrueValue):
					return true;
				case JsonToken.Integer when reader.Value.Equals(integerFalseValue):
					return false;
				case JsonToken.String when reader.Value.Equals(stringTrueValue):
					return true;
				case JsonToken.String when reader.Value.Equals(stringFalseValue):
					return false;
				default:
					throw new JsonSerializationException($"Can not read value ${reader.Value} as {objectType}");
			}
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(bool) || Nullable.GetUnderlyingType(objectType) == typeof(bool);
		}
	}
}
