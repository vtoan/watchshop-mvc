using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace aspcore_watchshop.Areas.Admin.Controllers {
    [Area ("Admin")]
    [Authorize]
    public class HomeController : Controller {
        public IActionResult Index () {
            return View ();
        }
    }
}