using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class SecurityQuestion
    {
        public SecurityQuestion()
        {
            UserQuestions = new HashSet<UserQuestion>();
        }

        public int QuestionId { get; set; }
        public string QuestionText { get; set; }

        public virtual ICollection<UserQuestion> UserQuestions { get; set; }
    }
}
