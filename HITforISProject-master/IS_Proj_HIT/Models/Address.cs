using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Address
    {
        public Address()
        {
            Facilities = new HashSet<Facility>();
            Insurances = new HashSet<Insurance>();
            PatientContactDetailMailingAddresses = new HashSet<PatientContactDetail>();
            PatientContactDetailResidenceAddresses = new HashSet<PatientContactDetail>();
            PatientEmergencyContacts = new HashSet<PatientEmergencyContact>();
            Physicians = new HashSet<Physician>();
        }

        public int AddressId { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int CountryId { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Country Country { get; set; }
        public virtual ICollection<Facility> Facilities { get; set; }
        public virtual ICollection<Insurance> Insurances { get; set; }
        public virtual ICollection<PatientContactDetail> PatientContactDetailMailingAddresses { get; set; }
        public virtual ICollection<PatientContactDetail> PatientContactDetailResidenceAddresses { get; set; }
        public virtual ICollection<PatientEmergencyContact> PatientEmergencyContacts { get; set; }
        public virtual ICollection<Physician> Physicians { get; set; }
    }
}
