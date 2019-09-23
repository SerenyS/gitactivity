using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class AlertType
    {
        public AlertType()
        {
            PatientAlerts = new HashSet<PatientAlerts>();
        }

        public int AlertId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string RestrictionType { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PatientAlerts> PatientAlerts { get; set; }
    }
}
