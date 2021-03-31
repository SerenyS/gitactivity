using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class MedStateType
    {
        public MedStateType()
        {
            MedicineAdministrationInfos = new HashSet<MedicineAdministrationInfo>();
        }

        public long MedStateTypeId { get; set; }
        public string MedState { get; set; }

        public virtual ICollection<MedicineAdministrationInfo> MedicineAdministrationInfos { get; set; }
    }
}
