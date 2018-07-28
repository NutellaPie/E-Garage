using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SSD_Assignment.Models;

namespace SSDAssignment.Pages.Audit
{
    public class CreateModel : PageModel
    {
        private readonly SSD_Assignment.Models.ApplicationDbContext _context;

        public CreateModel(SSD_Assignment.Models.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public AuditRecord AuditRecord { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AuditRecords.Add(AuditRecord);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}