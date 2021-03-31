using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Insurance
    {
        public Insurance()
        {
            PaymentPrimaryInsurances = new HashSet<Payment>();
            PaymentSecondaryInsurances = new HashSet<Payment>();
        }

        public int InsuranceId { get; set; }
        public string Subscriber { get; set; }
        public string GroupName { get; set; }
        public int PaymentSourceId { get; set; }
        public int PaymentPlanId { get; set; }
        public string InsuranceName { get; set; }
        public int AddressId { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Address Address { get; set; }
        public virtual PaymentPlan PaymentPlan { get; set; }
        public virtual PaymentSource PaymentSource { get; set; }
        public virtual ICollection<Payment> PaymentPrimaryInsurances { get; set; }
        public virtual ICollection<Payment> PaymentSecondaryInsurances { get; set; }
    }
}
