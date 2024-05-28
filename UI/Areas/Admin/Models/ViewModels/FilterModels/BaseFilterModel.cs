using Microsoft.AspNetCore.Routing;

namespace UI.Areas.Admin.Models.ViewModels.FilterModels
{
	public class BaseFilterModel
	{
		public RouteValueDictionary Merge(object obj)
		{
			var result = new RouteValueDictionary(this);
			foreach (var item in new RouteValueDictionary(obj))
				result[item.Key] = item.Value;
			return result;
		}
	}
}