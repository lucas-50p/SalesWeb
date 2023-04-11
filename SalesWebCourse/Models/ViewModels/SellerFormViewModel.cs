using System;
using System.Collections.Generic;
using System.Linq;

namespace SalesWebCourse.Models.ViewModels {
    public class SellerFormViewModel {

        public Seller Seller { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
