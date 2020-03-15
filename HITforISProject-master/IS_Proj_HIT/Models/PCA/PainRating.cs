using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models.PCA
{
    public class PainRating
    {
        public PainRating()
        {
            PainAssessments = new HashSet<PcaPainAssessment>();
        }

        public int PainRatingId { get; set; }
        public int PainParameterId { get; set; }
        public byte Value { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public virtual PainParameter PainParameter { get; set; }
        public virtual ICollection<PcaPainAssessment> PainAssessments { get; set; }
    }
}