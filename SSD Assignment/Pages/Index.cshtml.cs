using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SSD_Assignment.Pages
{
    public class IndexModel : PageModel
    {
        public string tempString { get; set; }
        public void OnGet()
        {
            tempString = "This is hidden, JJ #25/7/2018";
        }
    }
}
