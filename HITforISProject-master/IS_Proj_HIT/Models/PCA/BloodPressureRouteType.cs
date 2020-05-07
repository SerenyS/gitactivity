using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models.PCA
{
    public partial class BloodPressureRouteType
    {
        public BloodPressureRouteType()
        {
            PcaRecords = new HashSet<PcaRecord>();
        }

        public byte BloodPressureRouteTypeId { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }

        public ICollection<PcaRecord> PcaRecords { get; set; }
    }
}