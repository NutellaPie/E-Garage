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
using Microsoft.AspNetCore.Identity;
using System.Text;

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

            if (!(Listing.PhotoPath == "default-box.png") && (GetImageType(uploadedfilePath) != ""))
            {
                System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Listings", Listing.PhotoPath));
            }
            Listing.PhotoPath = fileName;
        }

        //File Upload Validation
        public static string GetImageType(string path)
        {
            string headerCode = GetHeaderInfo(path).ToUpper();

            if (headerCode.StartsWith("FFD8FFE0"))
            {
                return "JPG";
            }
            else if (headerCode.StartsWith("424D"))
            {
                return "BMP";
            }
            else if (headerCode.StartsWith("474946"))
            {
                return "GIF";
            }
            else if (headerCode.StartsWith("89504E470D0A1A0A"))
            {
                return "PNG";
            }
            else
            {
                return ""; //UnKnown
            }
        }

        public static string GetHeaderInfo(string path)
        {
            byte[] buffer = new byte[8];

            BinaryReader reader = new BinaryReader(new FileStream(path, FileMode.Open));
            reader.Read(buffer, 0, buffer.Length);
            reader.Close();

            StringBuilder sb = new StringBuilder();
            foreach (byte b in buffer)
                sb.Append(b.ToString("X2"));

            return sb.ToString();
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

            //Check if user is authenticated to edit
            if ((User.Identity.Name != Listing.UserName) && !(User.IsInRole("Admin")))
            {
                return RedirectToPage("../Account/AccessDenied");
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

            //File Upload path
            var DirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Listings");
            var FinalPath = Path.Combine(DirectoryPath, Listing.PhotoPath);

            //Validate uploaded photo
            if (GetImageType(FinalPath) == "")
            {
                TempData["notice"] = "Please upload a valid file";
                return Page();
            }

            _context.Attach(Listing).State = EntityState.Modified;
            

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!ListingExists(Listing.ID))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

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
