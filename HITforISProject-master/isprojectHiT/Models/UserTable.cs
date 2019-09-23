using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class UserTable
    {
        public UserTable()
        {
            UserFacility = new HashSet<UserFacility>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProgramEnrolledIn { get; set; }
        public string Instructor { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<UserFacility> UserFacility { get; set; }
    }
}
