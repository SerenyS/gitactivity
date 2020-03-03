using IS_Proj_HIT.Models;
using IS_Proj_HIT.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace isprojectHiT.Models
{
    public interface IWCTCHealthSystemRepository
    {
        #region IQueryables

        IQueryable<Pcarecord> PcaRecords { get; }
        IQueryable<Pcacomment> PcaComments { get; }
        IQueryable<PcacommentType> PcaCommentTypes { get; }
        IQueryable<CareSystemAssessment> SystemAssessments { get; }
        IQueryable<CareSystemAssessmentType> SystemAssessmentTypes { get; }
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

        void AddPcaRecord(Pcarecord pca);
        void DeletePcaRecord(Pcarecord pca);
        void EditPcaRecord(Pcarecord pca);
        
        void AddAssessment(CareSystemAssessment csa);
        void AddAssessments(IList<CareSystemAssessment> csaList);
        void DeleteAssessment(CareSystemAssessment csa);
        void EditAssessment(CareSystemAssessment csa);
    }
}