using System;
using System.Collections.Generic;
using System.Text;
using Tools.Finances.Sberbank.Enums;
using Tools.Finances.Sberbank.Requests;

namespace Tools.Finances.Sberbank.Serializers
{
	internal abstract class RequestSerializer
	{
		public abstract RequestSerializationFormat SerializationFormat { get; }

		public abstract string Serialize(BaseRequest request, string userName, string password);
	}
}
