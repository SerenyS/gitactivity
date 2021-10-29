using System.Collections.Generic;
using System.Linq;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;

namespace IS_Proj_HIT.Models.Data
{
    public class EFWCTCHealthSystemRepository : IWCTCHealthSystemRepository
    {
        private readonly WCTCHealthSystemContext _context;

        public EFWCTCHealthSystemRepository(WCTCHealthSystemContext context) => _context = context;

        #region IQueryable
        
        public IQueryable<CareSystemAssessment> CareSystemAssessments => _context.CareSystemAssessments;
        public IQueryable<CareSystemType> CareSystemAssessmentTypes => _context.CareSystemTypes;
        public IQueryable<CareSystemParameter> CareSystemParameters => _context.CareSystemParameters;
        public IQueryable<BloodPressureRouteType> BloodPressureRouteTypes => _context.BloodPressureRouteTypes;
        public IQueryable<Bmimethod> BmiMethods => _context.Bmimethods;
        public IQueryable<PcapainAssessment> PainAssessments => _context.PcapainAssessments;
        public IQueryable<PainRating> PainRatings => _context.PainRatings;
        public IQueryable<PainRatingImage> PainRatingImages => _context.PainRatingImages;
        public IQueryable<PainParameter> PainParameters => _context.PainParameters;
        public IQueryable<PainScaleType> PainScaleTypes => _context.PainScaleTypes;
        public IQueryable<O2deliveryType> O2DeliveryTypes => _context.O2deliveryTypes;
        public IQueryable<PulseRouteType> PulseRouteTypes => _context.PulseRouteTypes;
        public IQueryable<TempRouteType> TempRouteTypes => _context.TempRouteTypes;
        public IQueryable<Pcarecord> PcaRecords => _context.Pcarecords;
        public IQueryable<Pcacomment> PcaComments => _context.Pcacomments;
        public IQueryable<PcacommentType> PcaCommentTypes => _context.PcacommentTypes;
        public IQueryable<AdmitType> AdmitTypes => _context.AdmitTypes;
        public IQueryable<Ethnicity> Ethnicities => _context.Ethnicities;
        public IQueryable<Gender> Genders => _context.Genders;
        public IQueryable<AspNetUser> AspNetUsers => _context.AspNetUsers;
        public IQueryable<Department> Departments => _context.Departments;
        public IQueryable<EncounterType> EncounterTypes => _context.EncounterTypes;
        public IQueryable<Discharge> Discharges => _context.Discharges;
        public IQueryable<Sex> Sexes => _context.Sexes;
        public IQueryable<Patient> Patients => _context.Patients;
        public IQueryable<PlaceOfServiceOutPatient> PlaceOfService => _context.PlaceOfServiceOutPatients;
        public IQueryable<PointOfOrigin> PointOfOrigin => _context.PointOfOrigins;
        public IQueryable<Religion> Religions => _context.Religions;
        public IQueryable<MaritalStatus> MaritalStatuses => _context.MaritalStatuses;
        public IQueryable<PatientContactDetail> PatientContactDetails => _context.PatientContactDetails;
        public IQueryable<PatientAlert> PatientAlerts => _context.PatientAlerts;
        public IQueryable<AlertType> AlertTypes => _context.AlertTypes;
        public IQueryable<PatientRestriction> PatientRestrictions => _context.PatientRestrictions;
        public IQueryable<Employment> Employments => _context.Employments;
        public IQueryable<Encounter> Encounters => _context.Encounters;
        public IQueryable<FallRisk> FallRisks => _context.FallRisks;
        public IQueryable<Restriction> Restrictions => _context.Restrictions;
        public IQueryable<PatientFallRisk> PatientFallRisks => _context.PatientFallRisks;
        public IQueryable<Allergen> Allergens => _context.Allergens;
        public IQueryable<Reaction> Reactions => _context.Reactions;
        public IQueryable<PatientAllergy> PatientAllergy => _context.PatientAllergies;
        public IQueryable<EncounterPhysician> EncounterPhysicians => _context.EncounterPhysicians;
        public IQueryable<Facility> Facilities => _context.Facilities;
        public IQueryable<Physician> Physicians => _context.Physicians;
        public IQueryable<PhysicianRole> PhysicianRoles => _context.PhysicianRoles;
        public IQueryable<UserTable> UserTables => _context.UserTables;
        public IQueryable<Language> Languages => _context.Languages;
        public IQueryable<PatientLanguage> PatientLanguages => _context.PatientLanguages;
        public IQueryable<Race> Races => _context.Races;
        public IQueryable<PatientRace> PatientRaces => _context.PatientRaces;
        public IQueryable<Program> Programs => _context.Programs;
        public IQueryable<UserFacility> UserFacilities => _context.UserFacilities;
        public IQueryable<UserProgram> UserPrograms => _context.UserPrograms;
        public IQueryable<ProgramFacility> ProgramFacilities => _context.ProgramFacilities;
        public IQueryable<SecurityQuestion> SecurityQuestions => _context.SecurityQuestions;
        public IQueryable<UserSecurityQuestion> UserSecurityQuestions => _context.UserSecurityQuestions;




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
            PatientAlert pa = new PatientAlert();
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
                PatientFallRisk pfr = new PatientFallRisk();
                pfr.FallRiskId = alert.FallRiskId;
                pfr.PatientAlertId = patientAlertid;
                pfr.LastModified = alert.LastModified;
                _context.Attach(pfr);
                _context.SaveChanges();
            }
            else if (alertTypeid == 3)
            {
                PatientRestriction pr = new PatientRestriction();
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
                _context.PatientAllergies.Add(pall);
                _context.SaveChanges();
            }
            else
            {
                //do nothing else (Advanced Directive and Clinical Reminder only use PatientAlerts table)
            }
        }

        public void DeleteAlert(PatientAlert alert)
        {
            _context.Remove(alert);
            _context.SaveChanges();
        }

        public void EditAlert(PatientAlert alert)
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
        
        public void AddPcaComment(Pcacomment com)
        {
            _context.Add(com);
            _context.SaveChanges();
        }

        public void DeletePcaComment(Pcacomment com)
        {
            _context.Remove(com);
            _context.SaveChanges();
        }

        public void EditPcaComment(Pcacomment com)
        {
            _context.Update(com);
            _context.SaveChanges();
        }
        public void AddSystemAssessment(CareSystemAssessment csa)
        {
            _context.Add(csa);
            _context.SaveChanges();
        }

        public void AddSystemAssessments(IList<CareSystemAssessment> csaList)
        {
            csaList.ToList().ForEach(a => _context.Add(a));
            _context.SaveChanges();
        }

        public void DeleteSystemAssessment(CareSystemAssessment csa)
        {
            _context.Remove(csa);
            _context.SaveChanges();
        }

        public void EditSystemAssessment(CareSystemAssessment csa)
        {
            _context.Update(csa);
            _context.SaveChanges();
        }

        public void AddPainAssessment(PcapainAssessment pa)
        {
            _context.Add(pa);
            _context.SaveChanges();
        }

        public void DeletePainAssessment(PcapainAssessment pa)
        {
            _context.Remove(pa);
            _context.SaveChanges();
        }

        public void EditPainAssessment(PcapainAssessment pa)
        {
            _context.Update(pa);
            _context.SaveChanges();
        }

        public void AddPatientLanguage(PatientLanguage language)
        {
            _context.Add(language);
            _context.SaveChanges();
        }

        public void AddPatientRace(PatientRace race)
        {
            _context.Add(race);
            _context.SaveChanges();
        }

        public void DeletePatientRace(PatientRace race)
        {
            _context.Remove(race);
            _context.SaveChanges();
        }

        public void AddAddress(Address address)
        {
            _context.Add(address);
            _context.SaveChanges();
        }

        public void AddUserFacility(UserFacility userFacility)
        {
            _context.Add(userFacility);
            _context.SaveChanges();
        }

        public void DeleteUserFacility(UserFacility userFacility)
        {
            _context.Remove(userFacility);
            _context.SaveChanges();
        }

        public void EditUserFacility(UserFacility userFacility)
        {
            _context.Update(userFacility);
            _context.SaveChanges();
        }

        public void AddUserProgram(UserProgram userProgram)
        {
            _context.Add(userProgram);
            _context.SaveChanges();
        }

        public void DeleteUserProgram(UserProgram userProgram)
        {
            _context.Remove(userProgram);
            _context.SaveChanges();
        }

        public void EditUserProgram(UserProgram userProgram)
        {
            _context.Update(userProgram);
            _context.SaveChanges();
        }

        public void AddProgramFacility(ProgramFacility programFacility)
        {
            _context.Add(programFacility);
            _context.SaveChanges();
        }

        public void DeleteProgramFacility(ProgramFacility programFacility)
        {
            _context.Remove(programFacility);
            _context.SaveChanges();
        }

        public void EditProgramFacility(ProgramFacility programFacility)
        {
            _context.Update(programFacility);
        }
        public void AddAnswer(UserSecurityQuestion answer) {
            _context.Add(answer);
            _context.SaveChanges();
        }
        public void DeleteAnswer(UserSecurityQuestion answer) {
            _context.Remove(answer);
            _context.SaveChanges();
        }

        public void AddQuestion(SecurityQuestion question) {
            _context.Add(question);
            _context.SaveChanges();
        }
        public void DeleteQuestion(SecurityQuestion question) {
            _context.Remove(question);
            _context.SaveChanges();
        }
    }
}
