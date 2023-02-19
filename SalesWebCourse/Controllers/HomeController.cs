using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesWebCourse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebCourse.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

            public HomeController(ILogger<HomeController> logger) {
                _logger = logger;
            }

            public IActionResult Index() {
                return View();
            }

            public IActionResult About() {

                ViewData["Message"] = "Web MVC";
                ViewData["name"] = "lucas";

                return View();
            }

            public IActionResult Privacy() {

                return View();
            }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
