using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class BodySystemType
    {
        public BodySystemType()
        {
            ExamBodySystems = new HashSet<ExamBodySystem>();
            PhysicianAssessmentDetails = new HashSet<PhysicianAssessmentDetail>();
        }

        public short CareSystemTypeId { get; set; }
        public string Name { get; set; }
        public string NormalLimitsDescription { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual ICollection<ExamBodySystem> ExamBodySystems { get; set; }
        public virtual ICollection<PhysicianAssessmentDetail> PhysicianAssessmentDetails { get; set; }
    }
}
