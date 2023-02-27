using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SalesWebCourse.Models;
using SalesWebCourse.Models.ViewModels;

namespace SalesWebCourse.Data
{
    public class SalesWebCourseContext : DbContext
    {
        public SalesWebCourseContext (DbContextOptions<SalesWebCourseContext> options)
            : base(options)
        {
        }

        public DbSet<Department> Department { get; set; }
        public DbSet<Seller> Seller { get; set; }
        public DbSet<SalesRecord> SalesRecord { get; set; }
    }
}
