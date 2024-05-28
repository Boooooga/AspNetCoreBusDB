using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Tools.Finances.Sberbank.Other
{
	internal class UnixDateTimeConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			if (value == null)
				writer.WriteValue((object)null);
			writer.WriteValue((long)((DateTime)value - new DateTime(1970, 1, 1)).TotalMilliseconds);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return reader.Value == null ? (DateTime?)null : DateTime.SpecifyKind(new DateTime(1970, 1, 1), DateTimeKind.Utc).AddMilliseconds((long)reader.Value);
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || Nullable.GetUnderlyingType(objectType) == typeof(DateTime);
		}
	}
}
