using IS_Proj_HIT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace isprojectHiT.Models
{
    public class EFWCTCHealthSystemRepository : IWCTCHealthSystemRepository
    {
        private WCTCHealthSystemContext context;
        public EFWCTCHealthSystemRepository(WCTCHealthSystemContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Ethnicity> Ethnicities => context.Ethnicity;
        public IQueryable<Gender> Genders => context.Gender;

        public IQueryable<Discharge> Discharges => context.Discharge;
        public IQueryable<Sex> Sexes => context.Sex;

        public IQueryable<Patient> Patients => context.Patient;

        public IQueryable<Religion> Religions => context.Religion;

        public IQueryable<MaritalStatus> MaritalStatuses => context.MaritalStatus;

        public IQueryable<Employment> Employments => context.Employment;
        public IQueryable<Encounter> Encounters => context.Encounter;

        public void AddEmployment(Employment employment)
        {
            context.Add(employment);
            context.SaveChanges();
        }

        public void AddEncounter(Encounter encounter)
        {
            context.Add(encounter);
            context.SaveChanges();
        }

        public void AddPatient(Patient patient)
        {
            context.Add(patient);
            context.SaveChanges();
        }

        public void DeletePatient(Patient patient)
        {
            context.Remove(patient);
            context.SaveChanges();
        }

        public void EditPatient(Patient patient)
        {
            /*if (patient.Mrn == 0)
            {
                context.Patient.Add(patient);
            }
            else
            {*/
                context.Update(patient);
            //}

            context.SaveChanges();
        }
    }
}
