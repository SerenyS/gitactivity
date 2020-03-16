using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models.PCA
{
    public partial class PulseRouteType
    {
        public PulseRouteType()
        {
            Pcarecord = new HashSet<PcaRecord>();
        }

        public int PulseRouteTypeId { get; set; }
        public string PulseRouteTypeName { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PcaRecord> Pcarecord { get; set; }
    }
}
