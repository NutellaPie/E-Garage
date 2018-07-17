using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SSD_Assignment.Models
{
    public class ProfilePicture
    {
        public int ID { get; set; }
        public string Title { get; set; }

        public string ProfilePic { get; set; }
    }
}