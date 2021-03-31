using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class PreferredContactTime
    {
        public PreferredContactTime()
        {
            PatientContactDetails = new HashSet<PatientContactDetail>();
        }

        public int ContactTimeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PatientContactDetail> PatientContactDetails { get; set; }
    }
}
