using SalesWebCourse.Data;
using SalesWebCourse.Models.ViewModels;
using System.Linq;
using System.Collections.Generic;

namespace SalesWebCourse.Services {
    public class DepartmentService {


        private readonly SalesWebCourseContext _context;

        public DepartmentService(SalesWebCourseContext context) {
            _context = context;
        }

        // Retortar todos departamentos e ordenados
        public List<Department> FindAll() {
            return _context.Department.OrderBy(x=> x.Name).ToList();
        }
    }
}
