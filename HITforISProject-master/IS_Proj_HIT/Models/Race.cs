using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Race
    {
        public Race()
        {
            PatientRaces = new HashSet<PatientRace>();
        }

        public byte RaceId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PatientRace> PatientRaces { get; set; }
    }
}
