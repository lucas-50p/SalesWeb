using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebCourse.Models.ViewModels;

namespace SalesWebCourse.Data
{
    public class SalesWebCourseContext : DbContext
    {
        public SalesWebCourseContext (DbContextOptions<SalesWebCourseContext> options)
            : base(options)
        {
        }

        public DbSet<SalesWebCourse.Models.ViewModels.Department> Department { get; set; }
    }
}
