using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models
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

    /// <summary>
    /// Enum with existing System Assessment Types from the database
    /// </summary>
    public enum SystemAssessmentTypeEnum
    {
        Height = 2,
        Weight = 3,
        HeadCircumference = 9,
        BodyMassIndex = 10
    }
}