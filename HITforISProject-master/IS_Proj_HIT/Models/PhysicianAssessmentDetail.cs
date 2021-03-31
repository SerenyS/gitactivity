using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class PhysicianAssessmentDetail
    {
        public long PhysicianAssessmentDetailId { get; set; }
        public long? PhysicianAssessmentId { get; set; }
        public short? CareSystemTypeId { get; set; }
        public bool? IsWithinNormalLimits { get; set; }
        public string Comment { get; set; }
        public DateTime? LastModified { get; set; }
        public short? ExamTypeCode { get; set; }

        public virtual BodySystemType CareSystemType { get; set; }
        public virtual ExamType ExamTypeCodeNavigation { get; set; }
        public virtual PhysicianAssessment PhysicianAssessment { get; set; }
    }
}
