using SalesWebCourse.Data;
using SalesWebCourse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SalesWebCourse.Models.ViewModels;

namespace SalesWebCourse.Services {
    public class SalesRecordService {

        private readonly SalesWebCourseContext _context;

        public SalesRecordService(SalesWebCourseContext context) {
            _context = context;
        }

        // Tem uma operção assíncrona que busca os meus registros de venda por data
        public async Task<List<SalesRecord>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue) {
                result = result.Where(x => x.Date >= minDate.Value);
            }
            if (maxDate.HasValue) {
                result = result.Where(x => x.Date <= maxDate.Value);
            }
            return await result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        // Procurar por grupo
        public async Task<List<IGrouping<Department, SalesRecord>>> FindByDateGroupingAsync(DateTime? minDate, DateTime? maxDate) {
            var result = from obj in _context.SalesRecord select obj;
            if (minDate.HasValue) {
                result = result.Where(min => min.Date >= minDate.Value);
            }
            if (maxDate.HasValue) {
                result = result.Where(max => max.Date <= maxDate.Value);
            }
            return result
                .Include(x => x.Seller)
                .Include(x => x.Seller.Department)
                .OrderByDescending(x => x.Date)
                .ToList()
                .GroupBy(x => x.Seller.Department)
                .ToList();
        }
    }
}
