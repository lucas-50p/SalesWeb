using SalesWebCourse.Data;
using SalesWebCourse.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesWebCourse.Services {
    public class DepartmentService {


        private readonly SalesWebCourseContext _context;

        public DepartmentService(SalesWebCourseContext context) {
            _context = context;
        }

        // Retortar todos departamentos e ordenados
        public async Task<List<Department>> FindAllAsync() {
            return await _context.Department.OrderBy(x => x.Name).ToListAsync();
        }
    }
}
