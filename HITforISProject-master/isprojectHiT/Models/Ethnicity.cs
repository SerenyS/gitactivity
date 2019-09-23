using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class Ethnicity
    {
        public Ethnicity()
        {
            Patient = new HashSet<Patient>();
        }

        public byte EthnicityId { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }

        public IEnumerable<Ethnicity> Ethnicities { get; set; }
    }
}
