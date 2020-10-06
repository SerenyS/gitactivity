using System.Collections.Generic;
using System.Linq;
using IS_Proj_HIT.Models.PCA;
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
        IQueryable<BmiMethod> BmiMethods { get; }
        IQueryable<PcaPainAssessment> PainAssessments { get; }
        IQueryable<PainRating> PainRatings { get; }
        IQueryable<PainRatingImage> PainRatingImages { get; }
        IQueryable<PainParameter> PainParameters { get; }
        IQueryable<PainScaleType> PainScaleTypes { get; }
        IQueryable<O2deliveryType> O2DeliveryTypes { get; }
        IQueryable<PulseRouteType> PulseRouteTypes { get; }
        IQueryable<TempRouteType> TempRouteTypes { get; }
        IQueryable<PcaRecord> PcaRecords { get; }
        IQueryable<PcaComment> PcaComments { get; }
        IQueryable<PcaCommentType> PcaCommentTypes { get; }
        IQueryable<AdmitType> AdmitTypes { get; }
        IQueryable<Ethnicity> Ethnicities { get; }
        IQueryable<Gender> Genders { get; }
        IQueryable<Discharge> Discharges { get; }
        IQueryable<Departments> Departments { get; }
        IQueryable<EncounterType> EncounterTypes { get; }
        IQueryable<Sex> Sexes { get; }
        IQueryable<Religion> Religions { get; }
        IQueryable<MaritalStatus> MaritalStatuses { get; }
        IQueryable<Patient> Patients { get; }
        IQueryable<PlaceOfServiceOutPatient> PlaceOfService { get; }
        IQueryable<PointOfOrigin> PointOfOrigin { get; }
        IQueryable<Employment> Employments { get; }
        IQueryable<Encounter> Encounters { get; }
        IQueryable<EncounterPhysicians> EncounterPhysicians { get; }
        IQueryable<Facility> Facilities { get; }
        IQueryable<PatientContactDetails> PatientContactDetails { get; }
        IQueryable<Physician> Physicians { get; }
        IQueryable<PhysicianRole> PhysicianRoles { get; }
        IQueryable<PatientAlerts> PatientAlerts { get; }
        IQueryable<AlertType> AlertTypes { get; }
        IQueryable<PatientRestrictions> PatientRestrictions { get; }
        IQueryable<Restrictions> Restrictions { get; }
        IQueryable<PatientFallRisks> PatientFallRisks { get; }
        IQueryable<FallRisks> FallRisks { get; }
        IQueryable<Allergen> Allergens { get; }
        IQueryable<Reaction> Reactions { get; }
        IQueryable<PatientAllergy> PatientAllergy { get; }
        IQueryable<UserTable> UserTables { get; }
        IQueryable<Language> Languages { get; }
        IQueryable<PatientLanguage> PatientLanguages { get; }

        #endregion


        void AddPatient(Patient patient);
        void DeletePatient(Patient patient);
        void EditPatient(Patient patient);

        void AddEncounter(Encounter encounter);
        void EditEncounter(Encounter encounter);
        void DeleteEncounter(Encounter encounter);

        void AddAlert(AlertsViewModel alert);
        void EditAlert(PatientAlerts alert);

        void AddEmployment(Employment employment);

        void AddUser(UserTable userTable);
        void DeleteUser(UserTable userTable);
        void EditUser(UserTable userTable);

        void AddPcaRecord(PcaRecord pca);
        void DeletePcaRecord(PcaRecord pca);
        void EditPcaRecord(PcaRecord pca);
        
        void AddPcaComment(PcaComment pca);
        void DeletePcaComment(PcaComment pca);
        void EditPcaComment(PcaComment pca);

        void AddSystemAssessment(CareSystemAssessment csa);
        void AddSystemAssessments(IList<CareSystemAssessment> csaList);
        void DeleteSystemAssessment(CareSystemAssessment csa);
        void EditSystemAssessment(CareSystemAssessment csa);
        
        void AddPainAssessment(PcaPainAssessment pa);
        void DeletePainAssessment(PcaPainAssessment pa);
        void EditPainAssessment(PcaPainAssessment pa);
    }
}