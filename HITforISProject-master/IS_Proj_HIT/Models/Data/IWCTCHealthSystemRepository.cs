using System.Collections.Generic;
using System.Linq;
using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;

namespace IS_Proj_HIT.Models.Data
{
    public interface IWCTCHealthSystemRepository
    {
        #region IQueryables
        
        IQueryable<CareSystemAssessment> CareSystemAssessments { get; }
        IQueryable<CareSystemType> CareSystemAssessmentTypes { get; }
        IQueryable<CareSystemParameter> CareSystemParameters { get; }
        IQueryable<BloodPressureRouteType> BloodPressureRouteTypes { get; }
        IQueryable<Bmimethod> BmiMethods { get; }
        IQueryable<PcapainAssessment> PainAssessments { get; }
        IQueryable<PainRating> PainRatings { get; }
        IQueryable<PainRatingImage> PainRatingImages { get; }
        IQueryable<PainParameter> PainParameters { get; }
        IQueryable<PainScaleType> PainScaleTypes { get; }
        IQueryable<O2deliveryType> O2DeliveryTypes { get; }
        IQueryable<PulseRouteType> PulseRouteTypes { get; }
        IQueryable<TempRouteType> TempRouteTypes { get; }
        IQueryable<Pcarecord> PcaRecords { get; }
        IQueryable<Pcacomment> PcaComments { get; }
        IQueryable<PcacommentType> PcaCommentTypes { get; }
        IQueryable<AdmitType> AdmitTypes { get; }
        IQueryable<Ethnicity> Ethnicities { get; }
        IQueryable<Gender> Genders { get; }
        IQueryable<Discharge> Discharges { get; }
        IQueryable<Department> Departments { get; }
        IQueryable<EncounterType> EncounterTypes { get; }
        IQueryable<Sex> Sexes { get; }
        IQueryable<Religion> Religions { get; }
        IQueryable<MaritalStatus> MaritalStatuses { get; }
        IQueryable<Patient> Patients { get; }
        IQueryable<PlaceOfServiceOutPatient> PlaceOfService { get; }
        IQueryable<PointOfOrigin> PointOfOrigin { get; }
        IQueryable<Employment> Employments { get; }
        IQueryable<Encounter> Encounters { get; }
        IQueryable<EncounterPhysician> EncounterPhysicians { get; }
        IQueryable<Facility> Facilities { get; }
        IQueryable<PatientContactDetail> PatientContactDetails { get; }
        IQueryable<Physician> Physicians { get; }
        IQueryable<PhysicianRole> PhysicianRoles { get; }
        IQueryable<PatientAlert> PatientAlerts { get; }
        IQueryable<AlertType> AlertTypes { get; }
        IQueryable<PatientRestriction> PatientRestrictions { get; }
        IQueryable<Restriction> Restrictions { get; }
        IQueryable<PatientFallRisk> PatientFallRisks { get; }
        IQueryable<FallRisk> FallRisks { get; }
        IQueryable<Allergen> Allergens { get; }
        IQueryable<Reaction> Reactions { get; }
        IQueryable<PatientAllergy> PatientAllergy { get; }
        IQueryable<UserTable> UserTables { get; }
        IQueryable<Language> Languages { get; }
        IQueryable<PatientLanguage> PatientLanguages { get; }
        IQueryable<Race> Races { get; }
        IQueryable<PatientRace> PatientRaces { get; }
        IQueryable<Program> Programs { get; }
        IQueryable<UserFacility> UserFacilities { get; }
        IQueryable<UserProgram> UserPrograms { get; }
        IQueryable<ProgramFacility> ProgramFacilities { get; }
        IQueryable<SecurityQuestion> SecurityQuestions { get; }
        IQueryable<UserSecurityQuestion> UserSecurityQuestions { get; }

        #endregion

        void AddProgramFacility(ProgramFacility programFacility);
        void DeleteProgramFacility(ProgramFacility programFacility);
        void EditProgramFacility(ProgramFacility programFacility);

        void AddUserFacility(UserFacility userFacility);
        void DeleteUserFacility(UserFacility userFacility);
        void EditUserFacility(UserFacility userFacility);

        void AddUserProgram(UserProgram userProgram);
        void DeleteUserProgram(UserProgram userProgram);
        void EditUserProgram(UserProgram userProgram);

        void AddPatient(Patient patient);
        void DeletePatient(Patient patient);
        void EditPatient(Patient patient);

        void AddEncounter(Encounter encounter);
        void EditEncounter(Encounter encounter);
        void DeleteEncounter(Encounter encounter);

        void AddAlert(AlertsViewModel alert);
        void EditAlert(PatientAlert alert);
        void DeleteAlert(PatientAlert alert);

        void AddEmployment(Employment employment);

        void AddUser(UserTable userTable);
        void DeleteUser(UserTable userTable);
        void EditUser(UserTable userTable);

        void AddPcaRecord(Pcarecord pca);
        void DeletePcaRecord(Pcarecord pca);
        void EditPcaRecord(Pcarecord pca);
        
        void AddPcaComment(Pcacomment pca);
        void DeletePcaComment(Pcacomment pca);
        void EditPcaComment(Pcacomment pca);

        void AddSystemAssessment(CareSystemAssessment csa);
        void AddSystemAssessments(IList<CareSystemAssessment> csaList);
        void DeleteSystemAssessment(CareSystemAssessment csa);
        void EditSystemAssessment(CareSystemAssessment csa);
        
        void AddPainAssessment(PcapainAssessment pa);
        void DeletePainAssessment(PcapainAssessment pa);
        void EditPainAssessment(PcapainAssessment pa);

        void AddPatientLanguage(PatientLanguage language);
        void AddPatientRace(PatientRace race);
        void DeletePatientRace(PatientRace race);

        void AddAddress(Address address);

        void AddUserSecurityQuestion(UserSecurityQuestion userSecurityQuestion);
        void DeleteUserSecurityQuestion(UserSecurityQuestion userSecurityQuestion);

        void AddSecurityQuestion(SecurityQuestion securityQuestion);
        void DeleteSecurityQuestion(SecurityQuestion securityQuestion);
    }
}
