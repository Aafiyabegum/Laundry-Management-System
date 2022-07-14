using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryManagementSystem.Models
{
    public class OrderModel
    {

        public int OrderId { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? LastUpdatedDate { get; set; }
        public DateTime DeliveryDate { get; set; }
    }
}