using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class CareSystemAssessmentType
    {
        public CareSystemAssessmentType()
        {
            CareSystemAssessment = new HashSet<CareSystemAssessment>();
        }

        public int CareSystemAssessmentTypeId { get; set; }
        public string CareSystemAssessmentTypeName { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<CareSystemAssessment> CareSystemAssessment { get; set; }
    }
}
