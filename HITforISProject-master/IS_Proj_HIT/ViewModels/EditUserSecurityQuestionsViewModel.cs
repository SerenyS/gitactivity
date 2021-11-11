using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS_Proj_HIT.ViewModels
{
    public class EditUserSecurityQuestionsViewModel
    {
        public int SecurityQuestionId1 { get; set; }
        public string AnswerHash1 { get; set; }
        public int SecurityQuestionId2 { get; set; }
        public string AnswerHash2 { get; set; }
        public int SecurityQuestionId3 { get; set; }
        public string AnswerHash3 { get; set; }

        public int UserId { get; set; }
    }
}
