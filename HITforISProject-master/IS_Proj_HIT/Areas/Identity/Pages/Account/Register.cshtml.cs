using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace IS_Proj_HIT.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly IWCTCHealthSystemRepository _repository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public RegisterModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            ILogger<RegisterModel> logger,
            IWCTCHealthSystemRepository repo
            )
            
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
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

            //[Required]
            //[DataType(DataType.Text)]
            //[Display(Name = "StudentID")]
            //public string userID { get; set; }

            [BindProperty]
            public int ProgramId { get; set; }

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

        public List<SelectListItem> Programs { get; set; }
        public void OnGet(string returnUrl = null)
        {
            PopulateSelectList();
            
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            PopulateSelectList();

            returnUrl ??= Url.Content("~/Administration/EditRegisterDetails");
            if(!Input.PrivacyPolicyIsChecked)
                ModelState.AddModelError("Privacy", "Privacy Policy must be reviewed.");
            if (ModelState.IsValid)
            {
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

                    // Bind program and user
                    _repository.AddUserProgram(new UserProgram()
                    {
                        UserId = newUserTable.UserId,
                        ProgramId = Input.ProgramId
                    });

                    // Get all programs bound to user
                    var userPrograms = _repository.UserPrograms.Where(p => p.UserId == newUserTable.UserId).ToList();

                    if (userPrograms.Count == 1)
                    {
                        // All facilities are available based on program
                        var availableFacilities = _repository.ProgramFacilities.Where(p => p.ProgramId == Input.ProgramId).ToList();

                        Facility assignedFacility = null;
                        foreach (var facility in availableFacilities.Select(programFacility => _repository.Facilities.FirstOrDefault(f => f.FacilityId == programFacility.FacilityId))
                            .Where(facility => facility.Name.Contains("SIM")))
                        {
                            assignedFacility = facility;
                        }

                        // Bind user and facility
                        _repository.AddUserFacility(new UserFacility()
                        {
                            UserId = newUserTable.UserId,
                            FacilityId = assignedFacility.FacilityId,
                            LastModified = DateTime.Now,
                        });
                    }

                    return RedirectToPage("./AddSecurityQuestions", new { ReturnUrl = returnUrl, Id = newUserTable.UserId });
                }
                
                foreach (var error in result.Errors)
                {
                   ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private void PopulateSelectList()
        {
            Programs = _repository.Programs.Select(p => new SelectListItem()
            {
                Value = p.ProgramId.ToString(),
                Text = p.Name,
            }).ToList();
        }
    }
}
