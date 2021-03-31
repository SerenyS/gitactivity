using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Restriction
    {
        public Restriction()
        {
            PatientRestrictions = new HashSet<PatientRestriction>();
        }

        public short RestrictionId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<PatientRestriction> PatientRestrictions { get; set; }
    }
}
