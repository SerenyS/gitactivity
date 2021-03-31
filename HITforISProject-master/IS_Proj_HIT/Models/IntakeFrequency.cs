using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class IntakeFrequency
    {
        public IntakeFrequency()
        {
            ApprovedMedicinesToAdministers = new HashSet<ApprovedMedicinesToAdminister>();
        }

        public int IntakeFrequencyId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<ApprovedMedicinesToAdminister> ApprovedMedicinesToAdministers { get; set; }
    }
}
