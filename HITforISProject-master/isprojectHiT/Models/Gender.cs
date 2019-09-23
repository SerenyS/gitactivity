using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class Gender
    {
        public Gender()
        {
            Patient = new HashSet<Patient>();
        }

        public byte GenderId { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<Patient> Patient { get; set; }
    }
}
