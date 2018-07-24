using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SSD_Assignment.Data;
using SSD_Assignment.Services;
using Microsoft.AspNetCore.Hosting;
using SSD_Assignment.Models;
using System.IO;
using Microsoft.AspNetCore.Http;



namespace SSD_Assignment.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        //////////////////////PROPERTIES//////////////////////
        public string Username { get; set; }

        public bool IsEmailConfirmed { get; set; }

        public string FileName { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private IHostingEnvironment _environment;
        private readonly SSD_Assignment.Data.ApplicationDbContext _context;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            SSD_Assignment.Data.ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _context = context;
        }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [MaxLength(100)]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Profile Picture")]
            [BindProperty]
            public IFormFile ProfilePic { get; set; }
        }

        //////////////////////METHODS//////////////////////
        //private async Task UploadPhoto()
        //{
        //    var user = await _userManager.GetUserAsync(User);
        //    var fileName = Guid.NewGuid().ToString() + Input.ProfilePic.FileName;
        //    var uploadsDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/ProfilePics");
        //    var uploadedfilePath = Path.Combine(uploadsDirectoryPath, fileName);

        //    using (var fileStream = new FileStream(uploadedfilePath, FileMode.Create))
        //    {
        //        await Input.ProfilePic.CopyToAsync(fileStream);
        //    }

        //    user.ProfilePic = Input.ProfilePic;
        //}

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Username = user.UserName;

            Input = new InputModel
            {
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            FileName = user.ProfilePic;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);

            if (Input.ProfilePic != null)
            {
                var fileName = Guid.NewGuid().ToString() + Input.ProfilePic.FileName;
                var uploadsDirectoryPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/ProfilePics");
                var uploadedfilePath = Path.Combine(uploadsDirectoryPath, fileName);

                using (var fileStream = new FileStream(uploadedfilePath, FileMode.Create))
                {
                    user.ProfilePic = fileName;
                    await Input.ProfilePic.CopyToAsync(fileStream);
                }
            }

            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Input.Email != user.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting email for user with ID '{user.Id}'.");
                }
            }

            if (Input.PhoneNumber != user.PhoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    throw new ApplicationException($"Unexpected error occurred setting phone number for user with ID '{user.Id}'.");
                }
            }

            StatusMessage = "Your profile has been updated";
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostSendVerificationEmailAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = Url.EmailConfirmationLink(user.Id, code, Request.Scheme);
            await _emailSender.SendEmailConfirmationAsync(user.Email, callbackUrl);

            StatusMessage = "Verification email sent. Please check your email.";
            return RedirectToPage();
        }
    }
}

