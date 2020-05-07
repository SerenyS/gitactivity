using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace IS_Proj_HIT.Models.PCA
{
    public class CareSystemParameter
    {
        public short CareSystemParameterId { get; set; }
        public string Name { get; set; }
        public string NormalLimitsDescription { get; set; }
        public short CareSystemTypeId { get; set; }
        public DateTime LastModified { get; set; }

        public virtual CareSystemType CareSystemType { get; set; }
        public virtual ICollection<CareSystemAssessment> CareSystemAssessments { get; set; }
    }
}