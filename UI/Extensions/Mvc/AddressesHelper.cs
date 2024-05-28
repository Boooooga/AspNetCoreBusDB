using Microsoft.AspNetCore.Mvc;

namespace UI.Extensions.Mvc
{
	public static class AddressesExtensions
	{
		public static string GetArea(this IUrlHelper url)
		{
			return url.ActionContext.RouteData.Values["area"].ToString();
		}

		public static string GetController(this IUrlHelper url)
		{
			return url.ActionContext.RouteData.Values["controller"].ToString();
		}

		public static string GetAction(this IUrlHelper url)
		{
			return url.ActionContext.RouteData.Values["action"].ToString();
		}

		public static string GetImageUrl(this IUrlHelper url, string imageUrl, string placeholderUrl = "/images/adminImagePlaceholder.svg")
		{
			imageUrl ??= placeholderUrl;
			imageUrl = imageUrl.TrimStart('/');
			return url.Content("~/" + imageUrl);
		}
	}
}