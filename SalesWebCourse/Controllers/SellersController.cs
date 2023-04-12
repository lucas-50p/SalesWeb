﻿using Microsoft.AspNetCore.Mvc;
using SalesWebCourse.Services;
using SalesWebCourse.Models.ViewModels;
using SalesWebCourse.Models;

namespace SalesWebCourse.Controllers {
    public class SellersController : Controller {

        private readonly SellerService _sellersService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellersService, DepartmentService departmentService) {
            _sellersService = sellersService;
            _departmentService = departmentService;
        }

        public IActionResult Index() {
            // Retorna lista de vendedores
            var list = _sellersService.FindAll();
            return View(list);
        }

        // Vai abrir o formulario para cadastrar o vendedor
        public IActionResult Create() {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // Ela vai receber vendedor que veio da requisição
        [HttpPost]
        [ValidateAntiForgeryToken]//Seguraça
        public IActionResult Create(Seller seller) {
            _sellersService.Insert(seller);
            return RedirectToAction(nameof(Index));
        }

        // Delete confirmação, int? - opcional
        public IActionResult Delete(int? id) {
            //TODO
            if (id == null) {
                return NotFound();
            }

            // Busquei no BD
            var obj = _sellersService.FindById(id.Value);
            if (obj == null) {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Seguraça
        public IActionResult Delete(int id) {
            _sellersService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int? id) {
            //TODO
            if (id == null) {
                return NotFound();
            }

            // Busquei no BD
            var obj = _sellersService.FindById(id.Value);
            if (obj == null) {
                return NotFound();
            }
            return View(obj);
        }

    }
}
