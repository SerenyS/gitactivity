using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class ExamType
    {
        public ExamType()
        {
            ExamBodySystems = new HashSet<ExamBodySystem>();
            PhysicianAssessmentDetails = new HashSet<PhysicianAssessmentDetail>();
        }

        public short ExamTypeCode { get; set; }
        public string ExamType1 { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual ICollection<ExamBodySystem> ExamBodySystems { get; set; }
        public virtual ICollection<PhysicianAssessmentDetail> PhysicianAssessmentDetails { get; set; }
    }
}
