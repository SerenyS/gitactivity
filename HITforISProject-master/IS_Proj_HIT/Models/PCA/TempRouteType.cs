using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models.PCA
{
    public partial class TempRouteType
    {
        public TempRouteType()
        {
            Pcarecord = new HashSet<PcaRecord>();
        }

        public int TempRouteTypeId { get; set; }
        public string TempRouteTypeName { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PcaRecord> Pcarecord { get; set; }
    }
}
