using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public List<SelectListItem> Questions { get; set; }

        public SecurityQuestionsModel(IWCTCHealthSystemRepository repository)
        {
            _repository = repository;

            Questions = new List<SelectListItem>();
        }

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public int CurrentUserId { get; set; }

        public class InputModel : IValidatableObject
        {
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "first question")]
            public int SecurityQuestion1 { get; set;}

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "first answer")]
            public string SecurityQuestion1Answer { get; set; }

            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "second question")]
            public int SecurityQuestion2 { get; set;}
            
            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "second answer")]
            public string SecurityQuestion2Answer { get; set; }
            
            [Required]
            [DataType(DataType.Text)]
            [Display(Name = "third question")]
            public int SecurityQuestion3 { get; set;}

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "third answer")]
            public string SecurityQuestion3Answer { get; set; }

            public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
            {
                var questionList = new List<int>()
                {
                    SecurityQuestion1,
                    SecurityQuestion2,
                    SecurityQuestion3
                };

                var distinctResults = questionList.Distinct().ToList();

                if (distinctResults.Count != 3)
                {
                    yield return new ValidationResult("All questions must be unique!");
                }
            }
        }

        public void OnGet(int id, string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            PopulateSelectList();

            CurrentUserId = id;
        }

        public IActionResult OnPostAsync(int id, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                returnUrl ??= Url.Content("~/");
                CurrentUserId = id;

                var earlierQuestions = _repository.UserSecurityQuestions
                    .Where(q => q.UserId == CurrentUserId).ToList();
                if (earlierQuestions.Count != 0)
                {
                    foreach (var question in earlierQuestions)
                    {
                        _repository.DeleteUserSecurityQuestion(question);
                    }
                }

                var securityQuestions = new List<UserSecurityQuestion>
                {
                    new()
                    {
                        UserId = CurrentUserId,
                        SecurityQuestionId = Input.SecurityQuestion1,
                        AnswerHash = HashText(Input.SecurityQuestion1Answer)
                    },
                    new()
                    {
                        UserId = CurrentUserId,
                        SecurityQuestionId = Input.SecurityQuestion2,
                        AnswerHash = HashText(Input.SecurityQuestion2Answer)
                    },
                    new()
                    {
                        UserId = CurrentUserId,
                        SecurityQuestionId = Input.SecurityQuestion3,
                        AnswerHash = HashText(Input.SecurityQuestion3Answer)
                    }
                };

                foreach (var question in securityQuestions)
                {
                    _repository.AddUserSecurityQuestion(question);
                }

                return LocalRedirect(returnUrl);
            }

            PopulateSelectList();

            return Page();
        }

        private void PopulateSelectList()
        {
            Questions = _repository.SecurityQuestions.Select(q =>
            new SelectListItem
            {
                Value = q.SecurityQuestionId.ToString(),
                Text = q.QuestionText
            }).ToList();
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