using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class PaymentPlan
    {
        public PaymentPlan()
        {
            Insurances = new HashSet<Insurance>();
        }

        public int PaymentPlanId { get; set; }
        public string WiPopCode { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<Insurance> Insurances { get; set; }
    }
}
