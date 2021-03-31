using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Ethnicity
    {
        public Ethnicity()
        {
            Patients = new HashSet<Patient>();
        }

        public byte EthnicityId { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
