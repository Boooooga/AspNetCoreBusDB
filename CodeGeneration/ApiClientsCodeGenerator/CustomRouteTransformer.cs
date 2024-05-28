using SpaceApp.Dev.ApiToMobile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeGeneration.ApiClientsCodeGenerator
{
	internal class CustomRouteTransformer : IRouteTransformer
	{
		public string TransformRoute(MethodInfo action, string route)
		{
			return new Regex("v{.+:apiVersion}/").Replace(route, "");
		}
	}
}
