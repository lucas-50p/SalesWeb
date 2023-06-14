using Microsoft.AspNetCore.Mvc;
using SalesWebCourse.Services;
using SalesWebCourse.Models.ViewModels;
using SalesWebCourse.Models;
using System.Collections.Generic;
using SalesWebCourse.Services.Exceptions;
using System.Diagnostics;
using System;

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

            // Esse modelo server para testar ele valido
            if (!ModelState.IsValid) {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            _sellersService.Insert(seller);
                return RedirectToAction(nameof(Index));
        }

        // Delete confirmação, int? - opcional
        public IActionResult Delete(int? id) {
            //TODO
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }

            // Busquei no BD
            var obj = _sellersService.FindById(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
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
   
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }

            // Busquei no BD
            var obj = _sellersService.FindById(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            }
            return View(obj);
        }

        public IActionResult Edit(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }

            var obj = _sellersService.FindById(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            }

            List<Department> departments = _departmentService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel {
                Seller = obj,
                Departments = departments
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Seguraça
        public IActionResult Edit(int id, Seller seller) {

            if (!ModelState.IsValid) {
                var departments = _departmentService.FindAll();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id) {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch"});
            }

            try {
                _sellersService.Update(seller);
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }


        // "??" Operado de essencia nula
        public IActionResult Error(string message) {
            var viewModel = new ErrorViewModel {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };
            return View(viewModel);
        }
    }
}
