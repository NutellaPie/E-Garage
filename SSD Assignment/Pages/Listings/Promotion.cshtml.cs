using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment.Models;

namespace SSDAssignment.Pages.Listings
{
    [Authorize]
    public class PromotionModel : PageModel
    {
        private readonly SSD_Assignment.Models.ApplicationDbContext _context;

        public PromotionModel(SSD_Assignment.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        //public IActionResult OnGet()
        //{
        //    return Page();
        //}

        [BindProperty]
        public Promotion Promotion { get; set; }
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

            //Check if user is authenticated to edit
            if ((User.Identity.Name != Listing.UserName))
            {
                return RedirectToPage("../Account/AccessDenied");
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Listing = await _context.Listing.SingleOrDefaultAsync(m => m.ID == id);

            Promotion.ID = 0;
            Promotion.ListingID = Listing.ID;

            _context.Promotion.Add(Promotion);
            Listing.Title = "(Promoted) " + Listing.Title;

            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }
    }
}