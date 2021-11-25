using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IS_Proj_HIT.Models.Data;
using IS_Proj_HIT.Models;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace IS_Proj_HIT.Areas.Identity.Pages.Account
{
    public class EnterSecurityQuestionsModel : PageModel
    {
        private readonly IWCTCHealthSystemRepository _repository;
        private readonly UserManager<IdentityUser> _userManager;

        public List<string> SelectedQuestions { get; set; }

        public List<UserSecurityQuestion> UserSecurityQuestions { get; set; }

        public EnterSecurityQuestionsModel(IWCTCHealthSystemRepository repository, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
            UserSecurityQuestions = new List<UserSecurityQuestion>();
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public int CurrentUserId { get; set; }

        public class InputModel
        {
            [Required]
            [Display(Name = "first answer")]
            public string AnswerOne { get; set; }

            [Required]
            [Display(Name = "second answer")]
            public string AnswerTwo { get; set; }

            [Required]
            [Display(Name = "third answer")]
            public string AnswerThree { get; set; }
        }

        public void OnGet(int id, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            CurrentUserId = id;

           var securityQuestions = _repository.SecurityQuestions.Select(q => 
            new SecurityQuestion
            {
                SecurityQuestionId = q.SecurityQuestionId,
                QuestionText = q.QuestionText,
            }).ToList();

            UserSecurityQuestions = _repository.UserSecurityQuestions.Where(q => q.UserId == CurrentUserId).ToList();

            var questions = from question in securityQuestions
                              join userSecurityQuestions in UserSecurityQuestions
                              on new { question.SecurityQuestionId }
                              equals new { userSecurityQuestions.SecurityQuestionId }
                              select question.QuestionText;

            SelectedQuestions = questions.ToList();
        }

        public async Task<IActionResult> OnPostAsync(int id, string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            CurrentUserId = id;

            var answerOneHashed = HashText(Input.AnswerOne);
            var answerTwoHashed = HashText(Input.AnswerTwo);
            var answerThreeHashed = HashText(Input.AnswerThree);

            UserSecurityQuestions = _repository.UserSecurityQuestions.Where(q => q.UserId == CurrentUserId).ToList();

            var correctAnswers = true;
            var matchingAnswers = UserSecurityQuestions.Where(a => a.AnswerHash == answerOneHashed || a.AnswerHash == answerTwoHashed
            || a.AnswerHash == answerThreeHashed).ToList();

            if (matchingAnswers.Count != 3)
            {
                correctAnswers = false;
            }

            if (!correctAnswers)
                return RedirectToPage("./IncorrectAnswers", new { Id = CurrentUserId, ReturnUrl = returnUrl });

            var userTable = _repository.UserTables.FirstOrDefault(u => u.UserId == CurrentUserId);
            var aspUser = await _userManager.FindByIdAsync(userTable.AspNetUsersId);
            var code = await _userManager.GeneratePasswordResetTokenAsync(aspUser);

            return RedirectToPage("./ResetPassword", new { code, Id = CurrentUserId });
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
