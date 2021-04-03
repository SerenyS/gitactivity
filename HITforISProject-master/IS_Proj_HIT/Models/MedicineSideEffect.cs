using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class MedicineSideEffect
    {
        public long MedicineId { get; set; }
        public int SideEffectId { get; set; }

        public virtual MedicineAdministrationInfo Medicine { get; set; }
        public virtual SideEffect SideEffect { get; set; }
    }
}
