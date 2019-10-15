using IS_Proj_HIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace isprojectHiT.Models
{
    public interface IWCTCHealthSystemRepository
    {
        IQueryable<Ethnicity> Ethnicities { get; }
        IQueryable<Gender> Genders { get; }
        IQueryable<Discharge> Discharges { get; }
        IQueryable<Sex> Sexes { get; }
        IQueryable<Religion> Religions { get; }
        IQueryable<MaritalStatus> MaritalStatuses { get; }
        IQueryable<Patient> Patients { get; }
        IQueryable<Employment> Employments { get; }
        IQueryable<Encounter> Encounters { get; }
        void AddPatient(Patient patient);
        void DeletePatient(Patient patient);
        void EditPatient(Patient patient);

        void AddEmployment(Employment employment);
        void AddEncounter(Encounter encounter);
    }
}
