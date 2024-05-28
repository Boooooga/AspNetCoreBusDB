using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Public.Controllers
{
	[Area("Public")]
    public class ErrorController : Controller
    {
        public IActionResult Index(int code)
        {
			return View(code);
		}
    }
}