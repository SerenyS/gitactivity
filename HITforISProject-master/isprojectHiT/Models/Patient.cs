using System;
using System.Collections.Generic;

namespace isprojectHiT.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Encounter = new HashSet<Encounter>();
            PatientAlerts = new HashSet<PatientAlerts>();
            PatientAllergy = new HashSet<PatientAllergy>();
            PatientContactDetails = new HashSet<PatientContactDetails>();
            PatientEmergencyContact = new HashSet<PatientEmergencyContact>();
            PatientFallRisks = new HashSet<PatientFallRisks>();
            PatientLanguage = new HashSet<PatientLanguage>();
            PatientMilitaryService = new HashSet<PatientMilitaryService>();
            PatientRace = new HashSet<PatientRace>();
            PatientRestrictions = new HashSet<PatientRestrictions>();
        }

        public string Mrn { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string MaidenName { get; set; }
        public short? ReligionId { get; set; }
        public string AliasFirstName { get; set; }
        public string AliasMiddleName { get; set; }
        public string AliasLastName { get; set; }
        public string MothersMaidenName { get; set; }
        public string Ssn { get; set; }
        public DateTime Dob { get; set; }
        public byte SexId { get; set; }
        public byte? GenderId { get; set; }
        public bool DeceasedLiving { get; set; }
        public bool InterpreterNeeded { get; set; }
        public byte MaritalStatusId { get; set; }
        public byte EthnicityId { get; set; }
        public int? EmploymentId { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Employment Employment { get; set; }
        public virtual Ethnicity Ethnicity { get; set; }
        public virtual Gender Gender { get; set; }
        public virtual MaritalStatus MaritalStatus { get; set; }
        public virtual Religion Religion { get; set; }
        public virtual Sex Sex { get; set; }
        public virtual ICollection<Encounter> Encounter { get; set; }
        public virtual ICollection<PatientAlerts> PatientAlerts { get; set; }
        public virtual ICollection<PatientAllergy> PatientAllergy { get; set; }
        public virtual ICollection<PatientContactDetails> PatientContactDetails { get; set; }
        public virtual ICollection<PatientEmergencyContact> PatientEmergencyContact { get; set; }
        public virtual ICollection<PatientFallRisks> PatientFallRisks { get; set; }
        public virtual ICollection<PatientLanguage> PatientLanguage { get; set; }
        public virtual ICollection<PatientMilitaryService> PatientMilitaryService { get; set; }
        public virtual ICollection<PatientRace> PatientRace { get; set; }
        public virtual ICollection<PatientRestrictions> PatientRestrictions { get; set; }
    }
}
