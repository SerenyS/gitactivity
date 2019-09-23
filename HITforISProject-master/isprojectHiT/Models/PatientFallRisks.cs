using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class PatientFallRisks
    {
        public PatientFallRisks()
        {
            PatientAlerts = new HashSet<PatientAlerts>();
        }

        public long FallRiskId { get; set; }
        public string Description { get; set; }
        public string Mrn { get; set; }
        public DateTime DateOfApplication { get; set; }
        public DateTime? DateOfRemoval { get; set; }
        public string Comments { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Patient MrnNavigation { get; set; }
        public virtual ICollection<PatientAlerts> PatientAlerts { get; set; }
    }
}
