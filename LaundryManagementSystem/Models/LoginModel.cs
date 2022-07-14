using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaundryManagementSystem.Models
{
    public class LoginModel
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }
        public string Type { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}