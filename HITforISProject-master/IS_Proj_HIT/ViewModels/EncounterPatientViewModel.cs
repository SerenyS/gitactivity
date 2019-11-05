using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS_Proj_HIT.ViewModels
{
    public class EncounterPatientViewModel
    {

        public EncounterPatientViewModel()
        { }
        public EncounterPatientViewModel(string Mrn, long EncounterId, DateTime AdmitDateTime,
            string FirstName, string LastName)
        {
            this.Mrn = Mrn;
            this.EncounterId = EncounterId;
            this.AdmitDateTime = AdmitDateTime;
            this.FirstName = FirstName;
            this.LastName = LastName;
        }
        public string Mrn { get; set; }
        public long EncounterId { get; set; }
        public DateTime AdmitDateTime { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
