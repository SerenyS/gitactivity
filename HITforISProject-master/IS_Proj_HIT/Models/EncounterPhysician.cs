using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class EncounterPhysician
    {
        public EncounterPhysician()
        {
            Encounters = new HashSet<Encounter>();
        }

        public long EncounterPhysiciansId { get; set; }
        public int PhysicianId { get; set; }
        public int PhysicianRoleId { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Physician Physician { get; set; }
        public virtual PhysicianRole PhysicianRole { get; set; }
        public virtual ICollection<Encounter> Encounters { get; set; }
    }
}
