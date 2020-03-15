using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models.PCA
{
    public class PcaPainAssessment
    {
        public long PainAssessmentId { get; set; }
        public int PcaId { get; set; }
        public int PainParameterId { get; set; }
        public int PainRatingId { get; set; }
        public DateTime LastModified { get; set; }
        
        public virtual PcaRecord PcaRecord { get; set; }
        public virtual PainParameter PainParameter { get; set; }
        public virtual PainRating PainRating { get; set; }
    }
}