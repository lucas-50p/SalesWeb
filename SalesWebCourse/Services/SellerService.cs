using SalesWebCourse.Data;
using System;
using SalesWebCourse.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebCourse.Services.Exceptions;

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
            // Para buscar tb o departamento
            return _context.Seller.Include(obj => obj.Department).FirstOrDefault(obj => obj.Id == id);
        }

        public void Remove(int id) {
            var obj = _context.Seller.Find(id);// Encontra o id
            _context.Seller.Remove(obj);// remover o obj
            _context.SaveChanges();//salva alteração no BD
        }

        public void Update(Seller obj) {

            // Encontra obj Id
            if (!_context.Seller.Any(x => x.Id == obj.Id)) {
                throw new NotFoundException("Id not found");
            }

            // Conflitos de concorrencia no DB,
            try {
                _context.Update(obj);
                _context.SaveChanges();
            }

            // Só que usando a minha exceção em nível de serviço isso aqui é muito importante pra segregar as camadas
            // a minha camada de serviço ela não vai propagar uma exceção do nível de acesso a dados se uma exceção
            catch (DbUpdateConcurrencyException e) {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }
    }
}
