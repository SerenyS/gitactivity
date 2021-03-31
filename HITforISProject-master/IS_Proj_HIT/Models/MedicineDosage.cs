using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class MedicineDosage
    {
        public long MedicineAdminId { get; set; }
        public int DosageId { get; set; }

        public virtual Dosage Dosage { get; set; }
        public virtual MedicineAdministrationInfo MedicineAdmin { get; set; }
    }
}
