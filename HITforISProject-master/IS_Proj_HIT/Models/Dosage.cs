using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Dosage
    {
        public Dosage()
        {
            MedicineDosages = new HashSet<MedicineDosage>();
        }

        public int DosageId { get; set; }
        public decimal Amount { get; set; }
        public string UnitOfMeasurement { get; set; }

        public virtual ICollection<MedicineDosage> MedicineDosages { get; set; }
    }
}
