using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Facility
    {
        public Facility()
        {
            Encounters = new HashSet<Encounter>();
            UserFacilities = new HashSet<UserFacility>();
        }

        public int FacilityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public string Phone { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Address Address { get; set; }
        public virtual ICollection<Encounter> Encounters { get; set; }
        public virtual ICollection<UserFacility> UserFacilities { get; set; }
    }
}
