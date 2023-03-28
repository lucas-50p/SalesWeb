using Microsoft.AspNetCore.Mvc;
using SalesWebCourse.Services;
using SalesWebCourse.Models;

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

        public IActionResult Create() {
            return View();
        }

        // Ela vai receber vendedor que veio da requisição
        [HttpPost]
        [ValidateAntiForgeryToken]//Seguraça
        public IActionResult Create(Seller seller) {
           _sellersService.Insert(seller);
           return RedirectToAction(nameof(Index));
        }
    }
}
