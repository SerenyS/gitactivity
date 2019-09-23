using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class PatientAllergy
    {
        public PatientAllergy()
        {
            PatientAlerts = new HashSet<PatientAlerts>();
        }

        public long PatientAllergyId { get; set; }
        public int? AllergenId { get; set; }
        public int? ReactionId { get; set; }
        public DateTime DateOfFirstReaction { get; set; }
        public DateTime? DateResolved { get; set; }
        public string Comments { get; set; }
        public string Mrn { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Allergen Allergen { get; set; }
        public virtual Patient MrnNavigation { get; set; }
        public virtual Reaction Reaction { get; set; }
        public virtual ICollection<PatientAlerts> PatientAlerts { get; set; }
    }
}
