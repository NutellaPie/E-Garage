using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SSD_Assignment.Models;
using SSD_Assignment.Data;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace SSD_Assignment.Pages.Listings
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly SSD_Assignment.Models.ApplicationDbContext _context;

        public DeleteModel(SSD_Assignment.Models.ApplicationDbContext context)
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

            //Check if user is authenticated to delete
            if ((User.Identity.Name != Listing.UserName) && !(User.IsInRole("Admin")))
            {
                return RedirectToPage("../Account/AccessDenied");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Listing = await _context.Listing.FindAsync(id);

            if (Listing != null)
            {
                if (!(Listing.PhotoPath == "default-box.png"))
                {
                    System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Listings", Listing.PhotoPath));
                }
                _context.Listing.Remove(Listing);
                //await _context.SaveChangesAsync();

                // Once a record is deleted, create an audit record
                if (await _context.SaveChangesAsync() > 0)
                {
                    var auditrecord = new AuditRecord();
                    auditrecord.AuditActionType = "Delete Listing";
                    auditrecord.DateTimeStamp = DateTime.Now;
                    auditrecord.ListingID = Listing.ID;
                    var userID = User.Identity.Name.ToString();
                    auditrecord.Username = userID;
                    _context.AuditRecords.Add(auditrecord);
                    await _context.SaveChangesAsync();
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
