using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class MaritalStatus
    {
        public MaritalStatus()
        {
            Patients = new HashSet<Patient>();
        }

        public byte MaritalStatusId { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
