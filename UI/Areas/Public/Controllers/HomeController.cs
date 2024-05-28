using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;

namespace UI.Areas.Public.Controllers
{
	[Area("Public")]
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[Route("robots.txt")]
		public IActionResult Robots()
		{
			string filename;
			if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
			{
				filename = "robotsDevelopment.txt";
			}
			else
			{
				filename = "robotsProduction.txt";
			}
			
			string filepath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", filename);
			byte[] filedata = System.IO.File.ReadAllBytes(filepath);
			string contentType = "text/plain";

			return File(filedata, contentType);
		}
	}
}