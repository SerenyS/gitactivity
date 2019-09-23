using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class PhysicianRole
    {
        public PhysicianRole()
        {
            EncounterPhysicians = new HashSet<EncounterPhysicians>();
        }

        public int PhysicianRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<EncounterPhysicians> EncounterPhysicians { get; set; }
    }
}
