using SalesWebCourse.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace SalesWebCourse.Models {
    public class Seller {

        public int Id { get; set; }

        [Required (ErrorMessage ="{0} required")]// Campo obrigatorio, pega nome atributo
        [StringLength(60, MinimumLength = 3, ErrorMessage = "{0} size should be between {2} and {1}")]// Tamanho da string 
        public string Name { get; set; }

        [Required(ErrorMessage = "{0} required")]// Campo obrigatorio, pega nome atributo
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]// cria link
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")]// Campo obrigatorio, pega nome atributo
        [Display(Name="Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime BithDate { get; set; }

        [Required(ErrorMessage = "{0} required")]// Campo obrigatorio, pega nome atributo
        [Range(100.0, 5000.0, ErrorMessage = "{0} must be from {1} to {2}")]// minimo e maximo
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
