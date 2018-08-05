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
using Microsoft.AspNetCore.Authorization;
using System.IO;

namespace SSD_Assignment.Pages.Listings
{   
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly SSD_Assignment.Models.ApplicationDbContext _context;
        private async Task UploadPhoto()
        {
            var fileName = Guid.NewGuid().ToString() + Listing.Photo.FileName;
            var uploadsDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Listings");
            var uploadedfilePath = Path.Combine(uploadsDirectoryPath, fileName);

            using (var fileStream = new FileStream(uploadedfilePath, FileMode.Create))
            {
                await Listing.Photo.CopyToAsync(fileStream);
            }

            System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Listings", Listing.PhotoPath));
            Listing.PhotoPath = fileName;
        }

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

            if (!(Listing.Photo == null))
            {
                await UploadPhoto();
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

            //if listing is edited, create record
            if (await _context.SaveChangesAsync() > 0)
            {
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Edit Listing";
                auditrecord.DateTimeStamp = DateTime.Now;
                auditrecord.ListingID = Listing.ID;
                var userID = User.Identity.Name.ToString();
                auditrecord.Username = userID;
                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        private bool ListingExists(int id)
        {
            return _context.Listing.Any(e => e.ID == id);
        }
    }
}
