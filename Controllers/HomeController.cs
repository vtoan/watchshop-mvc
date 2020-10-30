using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace aspcore_watchshop.Controllers {
    public class HomeController : Controller {
        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Index () {
            return View ();
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error (int statusCode) {
            ViewBag.StatusCode = statusCode;
            ViewBag.Error = ReasonPhrases.GetReasonPhrase (statusCode);
            return View ();
        }
    }
}