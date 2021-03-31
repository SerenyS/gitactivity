using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class GenericMedicine
    {
        public GenericMedicine()
        {
            Medicines = new HashSet<Medicine>();
        }

        public long GenericMedId { get; set; }
        public string GenericName { get; set; }

        public virtual ICollection<Medicine> Medicines { get; set; }
    }
}
