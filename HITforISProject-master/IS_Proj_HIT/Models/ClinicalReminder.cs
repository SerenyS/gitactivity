using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class ClinicalReminder
    {
        public ClinicalReminder()
        {
            PatientClinicalReminders = new HashSet<PatientClinicalReminder>();
        }

        public short ClinicalReminderId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<PatientClinicalReminder> PatientClinicalReminders { get; set; }
    }
}
