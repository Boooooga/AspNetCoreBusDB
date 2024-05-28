using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Tools.Misc.SmsRu.Other
{
	internal class BooleanToIntConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
			{
				writer.WriteValue((object)null);
				return;
			}
			writer.WriteValue((bool)value ? 1 : 0);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return reader.Value == null ? (bool?) null : (int) reader.Value != 0;
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(bool) || Nullable.GetUnderlyingType(objectType) == typeof(bool);
		}
	}
}
