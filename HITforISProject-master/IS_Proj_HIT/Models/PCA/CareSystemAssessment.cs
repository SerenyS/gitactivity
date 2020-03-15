using System;

namespace IS_Proj_HIT.Models.PCA
{
    public partial class CareSystemAssessment
    {
        public int CareSystemAssessmentId { get; set; }
        public int PcaId { get; set; }
        public short CareSystemParameterId { get; set; }
        public bool IsWithinNormalLimits { get; set; }
        public string Comment { get; set; }
        public DateTime LastModified { get; set; }

        public virtual CareSystemParameter CareSystemParameter { get; set; }
        public virtual PcaRecord Pca { get; set; }
    }
}
