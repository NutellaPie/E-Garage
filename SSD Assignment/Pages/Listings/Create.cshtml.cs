using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SSD_Assignment.Models;
using SSD_Assignment.Data;
using System.IO;

namespace SSD_Assignment.Pages.Listings
{
    public class CreateModel : PageModel
    {
        private readonly SSD_Assignment.Data.ApplicationDbContext _context;
        private async Task UploadPhoto()
        {
            var uploadsDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads");
            var uploadedfilePath = Path.Combine(uploadsDirectoryPath, Listing.Photo.FileName);

            using (var fileStream = new FileStream(uploadedfilePath, FileMode.Create))
            {
                await Listing.Photo.CopyToAsync(fileStream);
            }
        }
        public CreateModel(SSD_Assignment.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Listing Listing { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Listing.PhotoPath = Listing.Photo.FileName;
            await UploadPhoto();

            _context.Listing.Add(Listing);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}