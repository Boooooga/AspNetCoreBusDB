using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace Tools.Finances.Sberbank.Other
{
	internal class FirstLowerContractResolver : DefaultContractResolver
	{
		protected override string ResolvePropertyName(string propertyName)
		{
			return propertyName[0].ToString().ToLowerInvariant() + propertyName.Substring(1);
		}
	}
}
