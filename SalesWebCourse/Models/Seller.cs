using SalesWebCourse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SalesWebCourse.Models {
    public class Seller {

        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]// cria link
        public string Email { get; set; }

        [Display(Name="Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BithDate { get; set; }

        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]// Formata duas casas decimais
        public double BaseSalary { get; set; }
        public Department Department { get; set; }
        public int DepartmentId { get; set; }// Garantir id não seja nulo, no banco de dados
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller() { 
        }

        public Seller(int id, string name, string email, DateTime bithDate, double baseSalary, Department department) {
            Id = id;
            Name = name;
            Email = email;
            BithDate = bithDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr) {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr) {
            Sales.Remove(sr);
        }

        public double TotalSales(DateTime initial, DateTime final) {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
