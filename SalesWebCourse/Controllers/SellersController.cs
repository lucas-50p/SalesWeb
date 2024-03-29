﻿using Microsoft.AspNetCore.Mvc;
using SalesWebCourse.Services;
using SalesWebCourse.Models.ViewModels;
using SalesWebCourse.Models;
using System.Collections.Generic;
using SalesWebCourse.Services.Exceptions;
using System.Diagnostics;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebCourse.Controllers {
    public class SellersController : Controller {

        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService) {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        public async Task <IActionResult> Index() {

            // Retorna lista de vendedores
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        // Vai abrir o formulario para cadastrar o vendedor
        public async Task<IActionResult> Create() {
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // Ela vai receber vendedor que veio da requisição
        [HttpPost]
        [ValidateAntiForgeryToken]//Seguraça
        public async Task<IActionResult> Create(Seller seller) {

            // Esse modelo server para testar ele valido
            if (!ModelState.IsValid) {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }
            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));
        }

        // Delete confirmação, int? - opcional
        public async Task<IActionResult> Delete(int? id) {
       
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }

            // Busquei no BD
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Seguraça
        public async Task<IActionResult> Delete(int id) {

            try {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e) {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        public async Task<IActionResult> Details(int? id) {
   
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }

            // Busquei no BD
            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            }
            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not provided"});
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null) {
                return RedirectToAction(nameof(Error), new { message = "Id not found"});
            }

            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel {
                Seller = obj,
                Departments = departments
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]//Seguraça
        public async Task<IActionResult> Edit(int id, Seller seller) {

            if (!ModelState.IsValid) {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (id != seller.Id) {
                return RedirectToAction(nameof(Error), new { message = "Id mismatch"});
            }

            try {
                await _sellerService.UpdateAsync(seller);
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
