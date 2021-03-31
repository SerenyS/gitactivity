using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class MedicineAdministrationInfo
    {
        public MedicineAdministrationInfo()
        {
            ApprovedMedicinesToAdministers = new HashSet<ApprovedMedicinesToAdminister>();
            MedicineDosages = new HashSet<MedicineDosage>();
            MedicineSideEffects = new HashSet<MedicineSideEffect>();
        }

        public long MedicineAdminId { get; set; }
        public long MedStateTypeId { get; set; }
        public long MedicineId { get; set; }
        public short? MinAge { get; set; }
        public short? MinWeight { get; set; }
        public int? AdminMethodId { get; set; }

        public virtual AdminMethod AdminMethod { get; set; }
        public virtual MedStateType MedStateType { get; set; }
        public virtual Medicine Medicine { get; set; }
        public virtual ICollection<ApprovedMedicinesToAdminister> ApprovedMedicinesToAdministers { get; set; }
        public virtual ICollection<MedicineDosage> MedicineDosages { get; set; }
        public virtual ICollection<MedicineSideEffect> MedicineSideEffects { get; set; }
    }
}
