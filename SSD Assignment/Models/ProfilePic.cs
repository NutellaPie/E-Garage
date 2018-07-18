using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSD_Assignment.Models
{
    public class ProfilePic
    {
        public int ID { get; set; }
        public string PhotoPath { get; set; }
        [NotMapped]
        public IFormFile Profilepicture { get; set; }

    }
}
