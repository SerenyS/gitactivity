using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class AlertType
    {
        public AlertType()
        {
            PatientAlerts = new HashSet<PatientAlert>();
        }

        public int AlertId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PatientAlert> PatientAlerts { get; set; }
    }
}
