using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CompletKitInstall.Areas.Identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CompletKitInstall.Pages
{
    [AllowAnonymous]
    public class ContactModel : PageModel
    {
        private readonly IMailer _emailSender;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        [BindProperty]
        [Required]
        public string UserEmail { get; set; }
        [BindProperty]
        [Required]
        public string EmailBody { get; set; }
        [BindProperty]
        [Required]
        public string EmailSubject { get; set; }

        public ContactModel(IMailer emailSender,SignInManager<IdentityUser> signInManager,UserManager<IdentityUser> userManager)
        {
            _emailSender = emailSender;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            await Task.CompletedTask;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            EmailBody = EmailBody + "/n" + $"This Email was sent to you from {UserEmail}.";
            await _emailSender.SendMailAsync("constantin.alixandroaie@gmail.com", EmailSubject,EmailBody);
            return RedirectToPage("/Contact");
        }

    }
}
