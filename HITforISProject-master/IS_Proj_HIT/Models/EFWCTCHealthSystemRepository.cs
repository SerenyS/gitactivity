using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace isprojectHiT.Models
{
    public class EFWCTCHealthSystemRepository : IWCTCHealthSystemRepository
    {
        private readonly WCTCHealthSystemContext _context;

        public EFWCTCHealthSystemRepository(WCTCHealthSystemContext context) => _context = context;

        #region IQueryable

        public IQueryable<TempRouteType> TempRouteTypes => _context.TempRouteType;
        public IQueryable<Pcarecord> PcaRecords => _context.Pcarecord;
        public IQueryable<Pcacomment> PcaComments => _context.Pcacomment;
        public IQueryable<PcacommentType> PcaCommentTypes => _context.PcacommentType;
        public IQueryable<AdmitType> AdmitTypes => _context.AdmitType;
        public IQueryable<Ethnicity> Ethnicities => _context.Ethnicity;
        public IQueryable<Gender> Genders => _context.Gender;
        public IQueryable<AspNetUsers> AspNetUsers => _context.AspNetUsers;
        public IQueryable<Departments> Departments => _context.Departments;
        public IQueryable<EncounterType> EncounterTypes => _context.EncounterType;
        public IQueryable<Discharge> Discharges => _context.Discharge;
        public IQueryable<Sex> Sexes => _context.Sex;
        public IQueryable<Patient> Patients => _context.Patient;
        public IQueryable<PlaceOfServiceOutPatient> PlaceOfService => _context.PlaceOfServiceOutPatient;
        public IQueryable<PointOfOrigin> PointOfOrigin => _context.PointOfOrigin;
        public IQueryable<Religion> Religions => _context.Religion;
        public IQueryable<MaritalStatus> MaritalStatuses => _context.MaritalStatus;
        public IQueryable<PatientContactDetails> PatientContactDetails => _context.PatientContactDetails;
        public IQueryable<PatientAlerts> PatientAlerts => _context.PatientAlerts;
        public IQueryable<AlertType> AlertTypes => _context.AlertType;
        public IQueryable<PatientRestrictions> PatientRestrictions => _context.PatientRestrictions;
        public IQueryable<Employment> Employments => _context.Employment;
        public IQueryable<Encounter> Encounters => _context.Encounter;
        public IQueryable<FallRisks> FallRisks => _context.FallRisks;
        public IQueryable<Restrictions> Restrictions => _context.Restrictions;
        public IQueryable<PatientFallRisks> PatientFallRisks => _context.PatientFallRisks;
        public IQueryable<Allergen> Allergens => _context.Allergen;
        public IQueryable<Reaction> Reactions => _context.Reaction;
        public IQueryable<PatientAllergy> PatientAllergy => _context.PatientAllergy;
        public IQueryable<EncounterPhysicians> EncounterPhysicians => _context.EncounterPhysicians;
        public IQueryable<Facility> Facilities => _context.Facility;
        public IQueryable<Physician> Physicians => _context.Physician;
        public IQueryable<PhysicianRole> PhysicianRoles => _context.PhysicianRole;
        public IQueryable<UserTable> UserTables => _context.UserTable;

        public IQueryable<CareSystemAssessment> SystemAssessments => _context.CareSystemAssessment;

        public IQueryable<CareSystemAssessmentType> SystemAssessmentTypes => _context.CareSystemAssessmentType;

        #endregion IQueryable

        public void AddPatient(Patient patient)
        {
            _context.Add(patient);
            _context.SaveChanges();
        }

        public void DeletePatient(Patient patient)
        {
            _context.Remove(patient);
            _context.SaveChanges();
        }

        public void EditPatient(Patient patient)
        {
            /*if (patient.Mrn == 0)
            {
                context.Patient.Add(patient);
            }
            else
            {*/
            _context.Update(patient);
            //}

            _context.SaveChanges();
        }

        public void AddEncounter(Encounter encounter)
        {
            _context.Add(encounter);
            _context.SaveChanges();
        }

        public void EditEncounter(Encounter encounter)
        {
            _context.Update(encounter);

            _context.SaveChanges();
        }

        public void DeleteEncounter(Encounter encounter)
        {
            _context.Remove(encounter);
            _context.SaveChanges();
        }

        public void AddAlert(AlertsViewModel alert)
        {
            PatientAlerts pa = new PatientAlerts();
            pa.AlertTypeId = alert.AlertTypeId;
            pa.PatientAlertId = alert.PatientAlertId;
            pa.Mrn = alert.Mrn;
            pa.LastModified = alert.LastModified;
            pa.StartDate = alert.StartDate;
            pa.EndDate = alert.EndDate;
            pa.Comments = alert.Comments;
            //context.PatientAlerts.Add(pa);
            _context.Attach(pa);
            _context.SaveChanges();
            long patientAlertid = pa.PatientAlertId;
            int? alertTypeid = pa.AlertTypeId;

            // Based on the alertTypeid above, decide which db table to save to...
            if (alertTypeid == 5)
            {
                PatientFallRisks pfr = new PatientFallRisks();
                pfr.FallRiskId = alert.FallRiskId;
                pfr.PatientAlertId = patientAlertid;
                pfr.LastModified = alert.LastModified;
                _context.Attach(pfr);
                _context.SaveChanges();
            }
            else if (alertTypeid == 3)
            {
                PatientRestrictions pr = new PatientRestrictions();
                pr.RestrictionTypeId = alert.RestrictionTypeId;
                pr.PatientAlertId = patientAlertid;
                pr.LastModified = alert.LastModified;
                _context.PatientRestrictions.Add(pr);
                _context.SaveChanges();
            }
            else if (alertTypeid == 4)
            {
                PatientAllergy pall = new PatientAllergy();
                pall.AllergenId = alert.AllergenId;
                pall.ReactionId = alert.ReactionId;
                pall.PatientAlertId = patientAlertid;
                pall.LastModified = alert.LastModified;
                _context.PatientAllergy.Add(pall);
                _context.SaveChanges();
            }
            else
            {
                //do nothing else (Advanced Directive and Clinical Reminder only use PatientAlerts table)
            }
        }

        public void EditAlert(PatientAlerts alert)
        {
            _context.Update(alert);
            _context.SaveChanges();
        }

        public void AddEmployment(Employment employment)
        {
            _context.Add(employment);
            _context.SaveChanges();
        }

        public void AddUser(UserTable userTable)
        {
            _context.Add(userTable);
            _context.SaveChanges();
        }

        public void DeleteUser(UserTable userTable)
        {
            _context.Remove(userTable);
            _context.SaveChanges();
        }

        public void EditUser(UserTable userTable)
        {
            _context.Update(userTable);
            _context.SaveChanges();
        }
        
        public void AddPcaRecord(Pcarecord pca)
        {
            _context.Add(pca);
            _context.SaveChanges();
        }

        public void DeletePcaRecord(Pcarecord pca)
        {
            _context.Remove(pca);
            _context.SaveChanges();
        }

        public void EditPcaRecord(Pcarecord pca)
        {
            _context.Update(pca);
            _context.SaveChanges();
        }

        public void AddAssessment(CareSystemAssessment csa)
        {
            _context.Add(csa);
            _context.SaveChanges();
        }

        public void AddAssessments(IList<CareSystemAssessment> csaList)
        {
            csaList.ToList().ForEach(a => _context.Add(a));
            _context.SaveChanges();
        }

        public void DeleteAssessment(CareSystemAssessment csa)
        {
            _context.Remove(csa);
            _context.SaveChanges();
        }

        public void EditAssessment(CareSystemAssessment csa)
        {
            _context.Update(csa);
            _context.SaveChanges();
        }
    }
}