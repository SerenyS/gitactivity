using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class MedicineUse
    {
        public long MedicineId { get; set; }
        public int UsesId { get; set; }

        public virtual Medicine Medicine { get; set; }
        public virtual Use Uses { get; set; }
    }
}
