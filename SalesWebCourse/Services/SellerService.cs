using SalesWebCourse.Data;
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
            _context.Add(obj);
            _context.SaveChanges();
        }

        public Seller FindById(int id) {
            return _context.Seller.FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id) {
            var obj = _context.Seller.Find(id);// Encontra o id
            _context.Seller.Remove(obj);// remover o obj
            _context.SaveChanges();//salva alteração no BD
        }
    }
}
