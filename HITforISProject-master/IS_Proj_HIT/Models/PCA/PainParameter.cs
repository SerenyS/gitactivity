using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models.PCA
{
    public class PainParameter
    {
        public PainParameter()
        {
            PainAssessments = new HashSet<PcaPainAssessment>();
            PainRatings = new HashSet<PainRating>();
        }

        public int PainParameterId { get; set; }
        public int PainScaleTypeId { get; set; }
        public string ParameterName { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public virtual PainScaleType PainScaleType { get; set; }
        public virtual ICollection<PcaPainAssessment> PainAssessments { get; set; }
        public virtual ICollection<PainRating> PainRatings { get; set; }
    }
}