using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IS_Proj_HIT.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWCTCHealthSystemRepository _repository;

        public ResetPasswordModel(UserManager<IdentityUser> userManager, IWCTCHealthSystemRepository repository)
        {
            _userManager = userManager;
            _repository = repository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public UserTable Table { get; set; }

        public class InputModel
        { 
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public IActionResult OnGet(int id, string code = null)
        {
            if (code == null)
            {
                return BadRequest("A code must be supplied for password reset.");
            }
            else
            {
                Input = new InputModel
                {
                    Code = code
                };
                Table = _repository.UserTables.FirstOrDefault(u => u.UserId == id);
                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Table = _repository.UserTables.FirstOrDefault(u => u.UserId == id);
            var user = await _userManager.FindByIdAsync(Table.AspNetUsersId);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }
    }
}
