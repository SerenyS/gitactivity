using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;

namespace IS_Proj_HIT.Models.PCA
{
    public class BloodPressureRouteType
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