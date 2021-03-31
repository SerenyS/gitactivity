using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class SideEffect
    {
        public SideEffect()
        {
            MedicineSideEffects = new HashSet<MedicineSideEffect>();
        }

        public int SideEffectId { get; set; }
        public string SideEffectDescription { get; set; }

        public virtual ICollection<MedicineSideEffect> MedicineSideEffects { get; set; }
    }
}
