using Microsoft.AspNetCore.Mvc;
using SalesWebCourse.Services;

namespace SalesWebCourse.Controllers {
    public class SellersController : Controller {

        private readonly SellerService _sellersService;

        public SellersController(SellerService sellersService) {
            _sellersService = sellersService;
        }

        public IActionResult Index() {
            // Retorna lista de vendedores
            var list = _sellersService.FindAll();
            return View(list);
        }
    }
}
