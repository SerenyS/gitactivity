using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class PaymentSource
    {
        public PaymentSource()
        {
            Insurances = new HashSet<Insurance>();
        }

        public int PaymentSourceId { get; set; }
        public string WiPopCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<Insurance> Insurances { get; set; }
    }
}
