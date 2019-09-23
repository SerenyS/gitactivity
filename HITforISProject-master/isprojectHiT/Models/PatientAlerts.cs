using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class PatientAlerts
    {
        public PatientAlerts()
        {
            EncounterAlerts = new HashSet<EncounterAlerts>();
        }

        public long PatientAlertId { get; set; }
        public int? AlertTypeId { get; set; }
        public long? PatientAllergyId { get; set; }
        public long? FallRiskId { get; set; }
        public long? RestrictionId { get; set; }
        public string Mrn { get; set; }
        public DateTime LastModified { get; set; }

        public virtual AlertType AlertType { get; set; }
        public virtual PatientFallRisks FallRisk { get; set; }
        public virtual Patient MrnNavigation { get; set; }
        public virtual PatientAllergy PatientAllergy { get; set; }
        public virtual PatientRestrictions Restriction { get; set; }
        public virtual ICollection<EncounterAlerts> EncounterAlerts { get; set; }
    }
}
