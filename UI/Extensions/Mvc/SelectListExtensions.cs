using System;
using System.Collections.Generic;
using System.Linq;
using Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.Extensions.Mvc
{
	public static class SelectListExtensions
	{
		public static IEnumerable<SelectListItem> GetEnumSelectList<TEnum>(this IHtmlHelper helper, IEnumerable<TEnum> items) where TEnum: Enum
		{
			return (items ?? typeof(TEnum).GetEnumValues().Cast<TEnum>()).Select(item => new SelectListItem
			{
				Text = item.GetDisplayName(),
				Value = item.ToString()
			});
		}

		public static IEnumerable<SelectListItem> GetNullableEnumSelectList<TEnum>(this IHtmlHelper helper, string nullTitle, IEnumerable<TEnum> items = null) where TEnum : Enum
		{
			var result = new List<SelectListItem>
			{
				new SelectListItem(nullTitle, "")
			};
			result.AddRange(helper.GetEnumSelectList(items));
			return result;
		}

		public static IEnumerable<SelectListItem> GetNullableBoolSelectList(this IHtmlHelper helper, string nullTitle)
		{
			return new List<SelectListItem>
			{
				new SelectListItem(nullTitle, ""),
				new SelectListItem("Да", true.ToString()),
				new SelectListItem("Нет", false.ToString())
			};
		}
	}
}
