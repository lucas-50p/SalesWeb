using Microsoft.AspNetCore.Mvc;

namespace SalesWebCourse.Controllers {
    public class SellersController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
