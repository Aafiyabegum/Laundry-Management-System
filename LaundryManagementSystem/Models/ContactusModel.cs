using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryManagementSystem.Models
{
   
    public class ContactusModel
    {
        public int ContatcId { get; set; }

        public string Name { get; set; }

        public string EmailId { get; set; }


        public int PhoneNo { get; set; }

        public string Subject { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }
    }
}