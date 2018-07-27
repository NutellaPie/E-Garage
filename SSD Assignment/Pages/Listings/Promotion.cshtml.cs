using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Promotion Promotion { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Promotion.Add(Promotion);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}