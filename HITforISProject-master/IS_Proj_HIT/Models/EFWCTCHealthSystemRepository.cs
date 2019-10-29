using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public IQueryable<AdmitType> AdmitTypes => context.AdmitType;
        public IQueryable<Ethnicity> Ethnicities => context.Ethnicity;
        public IQueryable<Gender> Genders => context.Gender;

        public IQueryable<Departments> Departments => context.Departments;
        public IQueryable<EncounterType> EncounterTypes => context.EncounterType;

        public IQueryable<Discharge> Discharges => context.Discharge;
        public IQueryable<Sex> Sexes => context.Sex;

        public IQueryable<Patient> Patients => context.Patient;
        public IQueryable<PlaceOfServiceOutPatient> PlaceOfService => context.PlaceOfServiceOutPatient;
        public IQueryable<PointOfOrigin> PointOfOrigin => context.PointOfOrigin;

        public IQueryable<Religion> Religions => context.Religion;

        public IQueryable<MaritalStatus> MaritalStatuses => context.MaritalStatus;

        public IQueryable<PatientContactDetails> PatientContactDetails => context.PatientContactDetails;

        public IQueryable<PatientAlerts> PatientAlerts => context.PatientAlerts;

        public IQueryable<AlertType> AlertTypes => context.AlertType;
        public IQueryable<PatientRestrictions> PatientRestrictions => context.PatientRestrictions;

        public IQueryable<Employment> Employments => context.Employment;
        public IQueryable<Encounter> Encounters => context.Encounter;

        public IQueryable<FallRisks> FallRisks => context.FallRisks;
        public IQueryable<Restrictions> Restrictions => context.Restrictions;

        public IQueryable<PatientFallRisks> PatientFallRisks => context.PatientFallRisks;


        public IQueryable<Allergen> Allergens => context.Allergen;

        public IQueryable<Reaction> Reactions => context.Reaction;

        public IQueryable<PatientAllergy> PatientAllergy => context.PatientAllergy;
        public IQueryable<EncounterPhysicians> EncounterPhysicians => context.EncounterPhysicians;
        public IQueryable<Facility> Facilities => context.Facility;
        public IQueryable<Physician> Physicians => context.Physician;

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

        public void AddAlert(AlertsViewModel alert)
        {

            //PatientAlerts pa = new PatientAlerts();
            //PatientFallRisks pfr = new PatientFallRisks();
            //using (var context = new WCTCHealthSystemContext())
            //{
            //    using (var transaction = context.Database.BeginTransaction())
            //    {
            //        try
            //        {
            //            // do your changes

            //            pa.AlertTypeId = alert.AlertTypeId;
            //            pa.PatientAlertId = alert.PatientAlertId;
            //            pa.Mrn = alert.Mrn;
            //            pa.LastModified = alert.LastModified;
            //            pa.StartDate = alert.StartDate;
            //            pa.EndDate = alert.EndDate;
            //            pa.Comments = alert.Comments;
            //            context.PatientAlerts.Add(pa);
            //            context.SaveChanges();

            //            pfr.FallRiskId = alert.FallRiskId;
            //            pfr.PatientAlertId = alert.PatientAlertId;
            //            pfr.FallRiskId = alert.FallRiskId;
            //            pfr.LastModified = alert.LastModified;
            //            context.PatientFallRisks.Add(pfr);
            //            context.SaveChanges();

            //            //            // do another changes
            //            //            context.SaveChanges();

            //            transaction.Commit();
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine("The following error appeared: " + ex.Message);
            //            transaction.Rollback();

            //            //Log, handle or absorbe I don't care ^_^
            //        }
            //    }
            //}


            Debug.WriteLine("this is fallRiskId " + alert.FallRiskId);
            Debug.WriteLine("this is alert.PatientAlertId " + alert.PatientAlertId);
            Debug.WriteLine("this is LastModified " + alert.LastModified);

            PatientAlerts pa = new PatientAlerts();
            pa.AlertTypeId = alert.AlertTypeId;
            pa.PatientAlertId = alert.PatientAlertId;
            pa.Mrn = alert.Mrn;
            pa.LastModified = alert.LastModified;
            pa.StartDate = alert.StartDate;
            pa.EndDate = alert.EndDate;
            pa.Comments = alert.Comments;
            context.PatientAlerts.Add(pa);
            context.SaveChanges();

            PatientFallRisks pfr = new PatientFallRisks();
            pfr.FallRiskId = alert.FallRiskId;
            pfr.PatientAlertId = alert.PatientAlertId;
            pfr.LastModified = alert.LastModified;
            context.PatientFallRisks.Add(pfr);
            context.SaveChanges();

            //PatientRestrictions pr = new PatientRestrictions();
            //pr.RestrictionTypeId = alert.RestrictionTypeId;
            //pr.PatientAlertId = alert.PatientAlertId;
            //pr.LastModified = alert.LastModified;
            //context.PatientRestrictions.Add(pr);
            //context.SaveChanges();
            //}

            //PatientAllergy pall = new PatientAllergy();
            //pall.AllergenId = alert.AllergenId;
            //pall.ReactionId = alert.ReactionId;
            //pall.PatientAlertId = alert.PatientAlertId;
            //pall.LastModified = alert.LastModified;
            //context.PatientAllergy.Add(pall);
            //context.SaveChanges();
        }

        }
}
