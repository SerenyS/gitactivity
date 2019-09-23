using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class UserFacility
    {
        public int UserId { get; set; }
        public int FacilityId { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Facility Facility { get; set; }
        public virtual UserTable User { get; set; }
    }
}
