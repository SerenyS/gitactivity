using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Warning
    {
        public Warning()
        {
            MedicineWarnings = new HashSet<MedicineWarning>();
        }

        public int WarningId { get; set; }
        public string WarningDescription { get; set; }

        public virtual ICollection<MedicineWarning> MedicineWarnings { get; set; }
    }
}
