using Microsoft.AspNetCore.Mvc;

namespace UI.Areas.Admin.Controllers
{
	[Area("Admin")]
    public class ErrorController : Controller
    {
        public IActionResult Index(int code)
        {
			return View(code);
		}
    }
}