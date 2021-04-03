using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class AdminMethod
    {
        public AdminMethod()
        {
            MedicineAdministrationInfos = new HashSet<MedicineAdministrationInfo>();
        }

        public int AdminMethodId { get; set; }
        public string AdminMethodName { get; set; }
        public string Instructions { get; set; }

        public virtual ICollection<MedicineAdministrationInfo> MedicineAdministrationInfos { get; set; }
    }
}
