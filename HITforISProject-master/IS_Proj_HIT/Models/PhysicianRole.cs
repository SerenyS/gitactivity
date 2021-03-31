using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class PhysicianRole
    {
        public PhysicianRole()
        {
            EncounterPhysicians = new HashSet<EncounterPhysician>();
        }

        public int PhysicianRoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<EncounterPhysician> EncounterPhysicians { get; set; }
    }
}
