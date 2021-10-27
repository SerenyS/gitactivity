using IS_Proj_HIT.Models;
using IS_Proj_HIT.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace IS_Proj_HIT.Areas.Identity.Pages.Account
{
    public class SecurityQuestions : PageModel
    {

        private readonly IWCTCHealthSystemRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public List<SelectListItem> Questions { get; } = new List<SelectListItem>{};

        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }

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

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        // getting current user is weird (securityquestions probably shouldnt be under Identity (and should probably be under Administration views or something similar) because it can be accessed without an account, which you would need an account for security questions) and i dont think answers will be hashed, other than that uncommenting the AddAnswers should make it work I think??
        public void OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Administration/EditRegisterDetails");
            var currentUser = _repository.UserTables.FirstOrDefault(u => u.Email == User.Identity.Name);
            var currentUserId = currentUser.UserId;

            var question1 = new UserSecurityQuestion { UserId = currentUserId, SecurityQuestionId = Input.SecurityQuestion1, AnswerHash = Input.SecurityQuestion1Answer };
            var question2 = new UserSecurityQuestion { UserId = currentUserId, SecurityQuestionId = Input.SecurityQuestion2, AnswerHash = Input.SecurityQuestion2Answer };
            var question3 = new UserSecurityQuestion { UserId = currentUserId, SecurityQuestionId = Input.SecurityQuestion3, AnswerHash = Input.SecurityQuestion3Answer };
            // _repository.AddAnswer(question1);
            // _repository.AddAnswer(question2);
            // _repository.AddAnswer(question3);
            _logger.LogInformation("Question 1: " + question1.UserId + " " + question1.SecurityQuestionId + " " + question1.AnswerHash);
        }

        // didnt have time to figure out how to fill the dropdowns without viewbag (moving securityquestions to views would fix this but it would also require some rewrites for sure)
        public void AddDropdown() {
            var sqList = _repository.SecurityQuestions
                .Select(n => new {n.SecurityQuestionId, n.QuestionText})
                .ToList();
            var questionList = new SelectList(sqList, "SecurityQuestionId", "QuestionText", 0);
            // foreach (var q in questionList) {
            //     Questions.Add(q);
            // }
        }
    }
}