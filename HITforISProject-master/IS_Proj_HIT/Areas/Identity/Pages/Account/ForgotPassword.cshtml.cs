using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IS_Proj_HIT.Models.Data;
using System.Linq;

//todo added this to fix emailsender
using Microsoft.AspNetCore.Identity.UI.Services;

namespace IS_Proj_HIT.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly IWCTCHealthSystemRepository _repository;

        public ForgotPasswordModel(UserManager<IdentityUser> userManager, IEmailSender emailSender, IWCTCHealthSystemRepository repository)
        {
            _userManager = userManager;
            _emailSender = emailSender;
            _repository = repository;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            if (!ModelState.IsValid) return Page();

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist or is not confirmed
                return RedirectToPage("./Login");
            }

            // For more information on how to enable account confirmation and password reset please 
            // visit https://go.microsoft.com/fwlink/?LinkID=532713
            //var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            //var callbackUrl = Url.Page(
            //    "/Account/ResetPassword",
            //    pageHandler: null,
            //    values: new { code },
            //    protocol: Request.Scheme);

            //await _emailSender.SendEmailAsync(
            //    Input.Email,
            //    "Reset Password",
            //    $"Please reset your password by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            var userTable = _repository.UserTables.FirstOrDefault(u => u.AspNetUsersId == user.Id);
            var questions = _repository.UserSecurityQuestions.Where(q => q.UserId == userTable.UserId).ToList();
            if (questions.Count == 0)
            {
                return RedirectToPage("./NoSecurityQuestions");
            }

            if (userTable != null)
                return RedirectToPage("./EnterSecurityQuestions",
                    new { Id = userTable.UserId, ReturnUrl = returnUrl });

            return Page();
        }
    }
}
