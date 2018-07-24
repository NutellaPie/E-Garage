using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment.Models;
using SSD_Assignment.Data;

namespace SSD_Assignment.Pages.Listings
{
    public class EditModel : PageModel
    {
        private readonly SSD_Assignment.Models.ApplicationDbContext _context;

        public EditModel(SSD_Assignment.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Listing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListingExists(Listing.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool ListingExists(int id)
        {
            return _context.Listing.Any(e => e.ID == id);
        }
    }
}
