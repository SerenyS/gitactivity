using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class ApprovedMedicinesToAdminister
    {
        public long ApprovedMedicineId { get; set; }
        public long EncounterId { get; set; }
        public long MedicineAdminId { get; set; }
        public long OrderInfoId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string SpecialInstructions { get; set; }
        public int IntakeFrequencyId { get; set; }

        public virtual Encounter Encounter { get; set; }
        public virtual IntakeFrequency IntakeFrequency { get; set; }
        public virtual MedicineAdministrationInfo MedicineAdmin { get; set; }
        public virtual OrderInfo OrderInfo { get; set; }
    }
}
