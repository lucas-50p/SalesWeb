﻿using SalesWebCourse.Data;
using System;
using SalesWebCourse.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebCourse.Services {
    public class SellerService {

        private readonly SalesWebCourseContext _context;

        public SellerService(SalesWebCourseContext context) {
            _context = context;
        }

        // Uma lista do vendedores do banco de dados
        public List<Seller> FindAll() {
            // Vai acessar fonte de dados da tabela e converte para lista
            return _context.Seller.ToList();
        }

        // Incluir um novo vendedor no bancos de dados
        public void Insert(Seller obj) {

            //TODO
            obj.Department = _context.Department.First();
            _context.Add(obj);
            _context.SaveChanges();
        }
    }
}
