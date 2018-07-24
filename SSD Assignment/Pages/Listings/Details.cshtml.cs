using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment.Models;

namespace SSD_Assignment.Pages.Listings
{
    public class DetailsModel : PageModel
    {
        private readonly SSD_Assignment.Models.ApplicationDbContext _context;

        public DetailsModel(SSD_Assignment.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public Listing Listing { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Listing = await _context.Listing.SingleOrDefaultAsync(m => m.ID == id);

            if (Listing == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
