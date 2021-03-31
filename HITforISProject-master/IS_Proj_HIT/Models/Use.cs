using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Use
    {
        public Use()
        {
            MedicineUses = new HashSet<MedicineUse>();
        }

        public int UsesId { get; set; }
        public string UseName { get; set; }

        public virtual ICollection<MedicineUse> MedicineUses { get; set; }
    }
}
