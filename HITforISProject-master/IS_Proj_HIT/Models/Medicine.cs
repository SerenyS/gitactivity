using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Medicine
    {
        public Medicine()
        {
            MedicineAdministrationInfos = new HashSet<MedicineAdministrationInfo>();
            MedicineIngredients = new HashSet<MedicineIngredient>();
            MedicineUses = new HashSet<MedicineUse>();
            MedicineWarnings = new HashSet<MedicineWarning>();
        }

        public long MedicineId { get; set; }
        public string BrandName { get; set; }
        public string Notes { get; set; }
        public decimal? Cost { get; set; }
        public long? GenericMedId { get; set; }

        public virtual GenericMedicine GenericMed { get; set; }
        public virtual ICollection<MedicineAdministrationInfo> MedicineAdministrationInfos { get; set; }
        public virtual ICollection<MedicineIngredient> MedicineIngredients { get; set; }
        public virtual ICollection<MedicineUse> MedicineUses { get; set; }
        public virtual ICollection<MedicineWarning> MedicineWarnings { get; set; }
    }
}
