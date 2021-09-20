using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using IS_Proj_HIT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Data;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IS_Proj_HIT.Models.Data;

namespace IS_Proj_HIT.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IWCTCHealthSystemRepository _repository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IWCTCHealthSystemRepository repo
            )
            
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _repository = repo;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "First Name")]
            public string FirstName { get; set; }
            
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Last Name")]
            public string LastName { get; set; }
            
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            // [Required]
            // [DataType(DataType.Text)]
            // [Display(Name = "StudentID")]
            // public string userID { get; set;}
            
            // [Required]
            // //DataTable dt = new DataTable();
            // public string PorgramEnrolledin { get; set; }
            
            // [Required]
            // [DataType(DataType.Text)]
            // public string program { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public bool PrivacyPolicyIsChecked { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Administration/EditRegisterDetails");
            if(!Input.PrivacyPolicyIsChecked)
                ModelState.AddModelError("Privacy", "Privacy Policy must be reveiwed.");
            if (ModelState.IsValid)
            {
                // var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                // var result = await _userManager.CreateAsync(user, Input.Password);
                // if (result.Succeeded)
                // {
                //     _logger.LogInformation("User created a new account with password.");
                //
                //     var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                //     var callbackUrl = Url.Page(
                //         "/Account/ConfirmEmail",
                //         pageHandler: null,
                //         values: new { userId = user.Id, code = code },
                //         protocol: Request.Scheme);
                //
                //     await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                //         $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                //
                //     await _signInManager.SignInAsync(user, isPersistent: false);
                //     return LocalRedirect(returnUrl);
                // }
                // foreach (var error in result.Errors)
                // {
                //     ModelState.AddModelError(string.Empty, error.Description);
                // }
                
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    var newUserTable = new UserTable
                    {
                        AspNetUsersId = user.Id,
                        Email = Input.Email,
                        LastModified = DateTime.Now,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                    };

                    _repository.AddUser(newUserTable);
                    return LocalRedirect(returnUrl);
                }
                
                foreach (var error in result.Errors)
                {
                   ModelState.AddModelError(string.Empty, error.Description);
                }

                
            }
            

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
