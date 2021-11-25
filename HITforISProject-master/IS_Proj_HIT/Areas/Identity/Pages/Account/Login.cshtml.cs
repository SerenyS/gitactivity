using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models;

namespace IS_Proj_HIT.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly IWCTCHealthSystemRepository _repository;

        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, 
            UserManager<IdentityUser> userManager, IWCTCHealthSystemRepository repository)
        {
            _signInManager = signInManager;
            _logger = logger;
            _repository = repository;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User logged in.");

                    var userTable = _repository.UserTables.FirstOrDefault(u => u.Email == Input.Email);

                    var userPrograms = _repository.UserPrograms.Where(u => u.UserId == userTable.UserId).ToList();
                    if (userPrograms.Count == 1)
                    {
                        var aspNetUser = await _userManager.FindByIdAsync(userTable.AspNetUsersId);
                        var currentRoles = await _userManager.GetRolesAsync(aspNetUser);

                        // Determines if user is part of faculty(Non-admin)
                        var isFaculty = false;
                        foreach (var role in currentRoles)
                        {
                            if (role is "HIT Faculty" or "Nursing Faculty" && role != "Administrator")
                            {
                                isFaculty = true;
                            }
                        }

                        // Faculty chooses which facility they wish to use
                        if (isFaculty)
                        {
                            return RedirectToPage("./SelectFacility", new { Id = userTable.UserId, ReturnUrl = returnUrl });
                        }

                        var userFacility = _repository.UserFacilities.FirstOrDefault(f => f.UserId == userTable.UserId);
                        if (userFacility == null)
                        {
                            var programFacilities = _repository.ProgramFacilities.Where(p => p.ProgramId == userPrograms[0].ProgramId).ToList();
                            Facility assignedFacility = null;

                            // Determines which facility they are assigned to
                            // Since they are not faculty they will assigned to SIM
                            foreach (var programFacility in from programFacility in programFacilities 
                                                            let facility = _repository.Facilities.FirstOrDefault(f => f.FacilityId == programFacility.FacilityId)
                                                            where facility != null && facility.Name.Contains("SIM") select programFacility)
                            {
                                assignedFacility = programFacility.Facility;
                            }

                            if (assignedFacility != null)
                                _repository.AddUserFacility(new UserFacility()
                                {
                                    UserId = userTable.UserId,
                                    FacilityId = assignedFacility.FacilityId,
                                    LastModified = DateTime.Now
                                });
                        }
                        else
                        {
                            userFacility.LastModified = DateTime.Now;
                            _repository.EditUserFacility(userFacility);
                        }
                    }
                    else if (userPrograms.Count == 0)
                    {
                        if (userTable != null)
                            return RedirectToPage("./SetProgram", new { Id = userTable.UserId, ReturnUrl = returnUrl });
                    }

                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    _logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return Page();
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
