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
        public async Task<List<Seller>> FindAllAsync() {
            // Vai acessar fonte de dados da tabela e converte para lista
            return await _context.Seller.ToListAsync();
        }

        // Incluir um novo vendedor no bancos de dados
        public async Task InsertAsync(Seller obj) {
            _context.Add(obj);
            await _context.SaveChangesAsync();
        }

       // internal Task FindAllAsync() {
        //    throw new NotImplementedException();
      //  }

        public async Task <Seller> FindByIdAsync(int id) {
            // Para buscar tb o departamento
            return await _context.Seller.Include(obj => obj.Department).FirstOrDefaultAsync(obj => obj.Id == id);
        }

        public async Task RemoveAsync(int id) {
            var obj = await _context.Seller.FindAsync(id);// Encontra o id
            _context.Seller.Remove(obj);// remover o obj
            await _context.SaveChangesAsync();//salva alteração no BD
        }

        public async Task UpdateAsync(Seller obj) {

            bool hasAny = await _context.Seller.AnyAsync(x => x.Id == obj.Id);

            // Encontra obj Id
            if (!hasAny) {
                throw new NotFoundException("Id not found");
            }

            // Conflitos de concorrencia no DB,
            try {
                _context.Update(obj);
                await _context.SaveChangesAsync();
            }

            // Só que usando a minha exceção em nível de serviço isso aqui é muito importante pra segregar as camadas
            // a minha camada de serviço ela não vai propagar uma exceção do nível de acesso a dados se uma exceção
            catch (DbUpdateConcurrencyException e) {
                throw new DbUpdateConcurrencyException(e.Message);
            }
        }
    }
}
