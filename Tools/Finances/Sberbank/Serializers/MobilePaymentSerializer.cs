using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Tools.Finances.Sberbank.Enums;
using Tools.Finances.Sberbank.Other;
using Tools.Finances.Sberbank.Requests;

namespace Tools.Finances.Sberbank.Serializers
{
	internal class MobilePaymentSerializer : RequestSerializer
	{
		public override RequestSerializationFormat SerializationFormat => RequestSerializationFormat.Json;

		public override string Serialize(BaseRequest request, string userName, string password)
		{
			return JsonConvert.SerializeObject(request, new JsonSerializerSettings
			{
				ContractResolver = new FirstLowerContractResolver(),
				NullValueHandling = NullValueHandling.Ignore
			});
		}
	}
}
