using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models.PCA
{
    public partial class PainScaleType
    {
        public PainScaleType()
        {
            Pcarecord = new HashSet<PcaRecord>();
            PainParameters = new HashSet<PainParameter>();
        }

        public int PainScaleTypeId { get; set; }
        public string PainScaleTypeName { get; set; }
        public string UseDescription { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PcaRecord> Pcarecord { get; set; }
        public virtual ICollection<PainParameter> PainParameters { get; set; }
    }
}
