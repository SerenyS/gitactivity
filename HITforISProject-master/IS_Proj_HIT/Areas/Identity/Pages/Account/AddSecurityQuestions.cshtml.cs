using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace IS_Proj_HIT.Areas.Identity.Pages.Account
{
    public class SecurityQuestionsModel : PageModel
    {

        private readonly IWCTCHealthSystemRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public List<SelectListItem> Questions { get; set; }

        public SecurityQuestionsModel(ILogger<RegisterModel> logger, UserManager<IdentityUser> userManager, 
            IWCTCHealthSystemRepository repository)
        {
            _logger = logger;
            _repository = repository;
            _userManager = userManager;

            Questions = new List<SelectListItem>();
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public int CurrentUserId { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Security Question 1")]
            public int SecurityQuestion1 { get; set;}

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Security Question 1 Answer")]
            public string SecurityQuestion1Answer { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Security Question 2")]
            public int SecurityQuestion2 { get; set;}
            
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Security Question 2 Answer")]
            public string SecurityQuestion2Answer { get; set; }
            
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "Security Question 3")]
            public int SecurityQuestion3 { get; set;}

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Security Question 3 Answer")]
            public string SecurityQuestion3Answer { get; set; }
        }

        public void OnGet(int id, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            Questions = _repository.SecurityQuestions.Select(q =>
            new SelectListItem
            {
                Value = q.SecurityQuestionId.ToString(),
                Text = q.QuestionText
            }).ToList();

            CurrentUserId = id;
        }

        public IActionResult OnPostAsync(int id, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            CurrentUserId = id;

            var earlierQuestions = _repository.UserSecurityQuestions.Where(q => q.UserId == CurrentUserId).ToList();
            if (earlierQuestions.Count != 0)
            {
                foreach (var question in earlierQuestions)
                {
                    _repository.DeleteUserSecurityQuestion(question);
                }
            }

            var question1 = new UserSecurityQuestion { UserId = CurrentUserId, SecurityQuestionId = Input.SecurityQuestion1, 
                AnswerHash = HashText(Input.SecurityQuestion1Answer) };
            var question2 = new UserSecurityQuestion { UserId = CurrentUserId, SecurityQuestionId = Input.SecurityQuestion2, 
                AnswerHash = HashText(Input.SecurityQuestion2Answer) };
            var question3 = new UserSecurityQuestion { UserId = CurrentUserId, SecurityQuestionId = Input.SecurityQuestion3, 
                AnswerHash = HashText(Input.SecurityQuestion3Answer) };

            _repository.AddUserSecurityQuestion(question1);
            _repository.AddUserSecurityQuestion(question2);
            _repository.AddUserSecurityQuestion(question3);

            _logger.LogInformation("Question 1: " + question1.UserId + " " + question1.SecurityQuestionId + " " + question1.AnswerHash);

            return LocalRedirect(returnUrl);
        }

        private static string HashText(string text)
        {
            using var myHash = SHA256.Create();
            var byteRaw = Encoding.UTF8.GetBytes(text);
            var byteResult = myHash.ComputeHash(byteRaw);

            return string.Concat(Array.ConvertAll(byteResult, h => h.ToString("X2")));
        }
    }
}