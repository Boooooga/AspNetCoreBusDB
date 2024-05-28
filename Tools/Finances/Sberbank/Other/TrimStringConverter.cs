using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using Newtonsoft.Json;

namespace Tools.Finances.Sberbank.Other
{
	internal class TrimStringConverter : JsonConverter
	{
		public int MaxLength { get; }

		public TrimStringConverter(int maxLength)
		{
			MaxLength = maxLength;
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var s = value as string;
			if (s != null && s.Length > MaxLength)
				s = s.Substring(0, MaxLength);
			writer.WriteValue(s);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(string);
		}

		public override bool CanRead => false;
	}
}
