using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models
{
    public partial class Physician
    {
        public Physician()
        {
            EncounterPhysicians = new HashSet<EncounterPhysicians>();
        }

        public int PhysicianId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<EncounterPhysicians> EncounterPhysicians { get; set; }
    }
}
