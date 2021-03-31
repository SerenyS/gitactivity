using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class MedicineWarning
    {
        public long MedicineId { get; set; }
        public int WarningId { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Warning Warning { get; set; }
    }
}
