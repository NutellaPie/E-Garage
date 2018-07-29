using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSD_Assignment.Models
{
    public class Promotion
    {
        public int ID { get; set; }
        public double PromotionPackages { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int CardNumber { get; set; }
        public DateTime DateOfExpiry { get; set; }
        public int CVV { get; set; }
    }
}
