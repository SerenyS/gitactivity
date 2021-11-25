using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IS_Proj_HIT.Areas.Identity.Pages.Account
{
    public class IncorrectAnswersModel : PageModel
    {
        public void OnGet()
        {
        }

        public IActionResult OnPost(int id, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            return RedirectToPage("./EnterSecurityQuestions", new { Id = id, ReturnUrl = returnUrl });
        }
    }
}
