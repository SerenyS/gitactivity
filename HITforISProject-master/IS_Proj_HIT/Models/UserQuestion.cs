using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class UserQuestion
    {
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string AnswerHash { get; set; }

        public virtual SecurityQuestion Question { get; set; }
        public virtual UserTable User { get; set; }
    }
}
