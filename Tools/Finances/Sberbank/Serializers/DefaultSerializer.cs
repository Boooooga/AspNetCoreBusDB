using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Tools.Finances.Sberbank.Enums;
using Tools.Finances.Sberbank.Other;
using Tools.Finances.Sberbank.Requests;

namespace Tools.Finances.Sberbank.Serializers
{
	internal class DefaultSerializer : RequestSerializer
	{
		public override RequestSerializationFormat SerializationFormat => RequestSerializationFormat.WebForm;

		public override string Serialize(BaseRequest obj, string userName, string password)
		{
			var contractResolver = new FirstLowerContractResolver();
			var jObj = JObject.FromObject(obj, new JsonSerializer
			{
				ContractResolver = contractResolver,
				NullValueHandling = NullValueHandling.Ignore
			});
			var result = new StringBuilder();
			result.AppendFormat("userName={0}&password={1}&", HttpUtility.UrlEncode(userName), HttpUtility.UrlEncode(password));
			foreach (var property in obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
			{
				var serializedName = property.GetCustomAttributes(typeof(JsonPropertyAttribute))?.Cast<JsonPropertyAttribute>().FirstOrDefault()?.PropertyName ??
									 contractResolver.GetResolvedPropertyName(property.Name);
				var currentToken = jObj[serializedName];
				if (currentToken != null)
					result.AppendFormat("{0}={1}&", serializedName, HttpUtility.UrlEncode(currentToken is JObject ? 
						currentToken.ToString(Formatting.None) : currentToken.Value<string>()));
			}
			return result.ToString().TrimEnd('&');
		}
	}
}
