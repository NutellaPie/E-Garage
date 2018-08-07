using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SSD_Assignment.Models;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Text;


namespace SSD_Assignment.Pages.Listings
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
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

        public CreateModel(
            SSD_Assignment.Models.ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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

            if (Listing.Photo == null)
            {
                Listing.PhotoPath = "default-box.png";
            }
            else
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

            //Record User who created listing
            //Listing.UserID = Int32.Parse((await _userManager.GetUserAsync(HttpContext.User))?.Id);
            Listing.UserName = User.Identity.Name.ToString();

            //Recorde date listing is created
            Listing.PostedDateTime = DateTime.Now;

            _context.Listing.Add(Listing);
            //await _context.SaveChangesAsync();

            // Once a record is added, create an audit record
            if (await _context.SaveChangesAsync() > 0)
            {
                // Create an auditrecord object
                var auditrecord = new AuditRecord();
                auditrecord.AuditActionType = "Add Listing";
                auditrecord.DateTimeStamp = DateTime.Now;
                auditrecord.ListingID = Listing.ID;
                // Get current logged-in user
                var userID = User.Identity.Name.ToString();
                auditrecord.Username = userID;
                _context.AuditRecords.Add(auditrecord);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}