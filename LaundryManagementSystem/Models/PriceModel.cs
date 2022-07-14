using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryManagementSystem.Models
{
    public class PriceModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public decimal Price { get; set; }

        public int Lbs { get; set; }

        public int Day { get; set; }

        public string IsIron { get; set; }

        public string Perfume { get; set; }

        public string IsPressing { get; set; }

        public string IsDry { get; set; }
    }
}