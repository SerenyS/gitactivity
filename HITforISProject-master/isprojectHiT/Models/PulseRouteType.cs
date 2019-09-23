using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class PulseRouteType
    {
        public PulseRouteType()
        {
            Pcarecord = new HashSet<Pcarecord>();
        }

        public int PulseRouteTypeId { get; set; }
        public string PulseRouteTypeName { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<Pcarecord> Pcarecord { get; set; }
    }
}
