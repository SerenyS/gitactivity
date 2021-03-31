using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Reaction
    {
        public Reaction()
        {
            PatientAllergies = new HashSet<PatientAllergy>();
        }

        public int ReactionId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PatientAllergy> PatientAllergies { get; set; }
    }
}
