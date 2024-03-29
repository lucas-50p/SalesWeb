﻿using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;

namespace SalesWebCourse.Models.ViewModels {
    public class Department {

        public int Id { get; set; } 
        public string Name { get; set; }
        public ICollection<Seller> Sellers { get; set; } = new List<Seller>();
   
        public Department() { 
        }

        // Menos Seller por ser coleção
        public Department(int id, string name) {
            Id = id;
            Name = name;
        }

        public void Add(Seller seller) {
           Sellers.Add(seller);
        }

        public double TotalSales(DateTime initial, DateTime final) {
            return Sellers.Sum(seller => seller.TotalSales(initial, final));
        }
    }
}
