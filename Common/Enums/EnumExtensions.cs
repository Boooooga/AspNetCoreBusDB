using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Common.Enums
{
	public static class EnumExtensions
	{
		public static string GetDisplayName<T>(this T value) where T: Enum
		{
			return value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false)
				.Cast<DisplayAttribute>().FirstOrDefault()?.GetName() ?? value.ToString();
		}
	}
}