using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Encounter
    {
        public Encounter()
        {
            ApprovedMedicinesToAdministers = new HashSet<ApprovedMedicinesToAdminister>();
            EncounterAlerts = new HashSet<EncounterAlert>();
            OrderInfos = new HashSet<OrderInfo>();
            Pcarecords = new HashSet<Pcarecord>();
            PhysicianAssessments = new HashSet<PhysicianAssessment>();
            ProcedureReports = new HashSet<ProcedureReport>();
            ProgressNotes = new HashSet<ProgressNote>();
        }

        public string Mrn { get; set; }
        public long? EncounterRestrictionId { get; set; }
        public long EncounterId { get; set; }
        public int FacilityId { get; set; }
        public byte PatientCurrentAge { get; set; }
        public DateTime AdmitDateTime { get; set; }
        public string ChiefComplaint { get; set; }
        public int? EncounterTypeId { get; set; }
        public int PlaceOfServiceId { get; set; }
        public int AdmitTypeId { get; set; }
        public string RoomNumber { get; set; }
        public long EncounterPhysiciansId { get; set; }
        public bool FacilityRegistryOptInOut { get; set; }
        public int? DepartmentId { get; set; }
        public int PointOfOriginId { get; set; }
        public long? PaymentId { get; set; }
        public int? DischargeDisposition { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime? DischargeDateTime { get; set; }
        public string DischargeComment { get; set; }
        public int? PhysicianId { get; set; }
        public string AdmittingDiagnosis { get; set; }
        public string HistoryOfPresentIllness { get; set; }
        public string SignificantFindings { get; set; }
        public string MedicationsOnDischarge { get; set; }
        public string DischargeDietInstructions { get; set; }
        public int? CoSignature { get; set; }
        public DateTime? WrittenDateTime { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int? EditedBy { get; set; }
        public string DischargeDispositionNote { get; set; }
        public int? AuthoringProvider { get; set; }

        public virtual AdmitType AdmitType { get; set; }
        public virtual Physician AuthoringProviderNavigation { get; set; }
        public virtual Physician CoSignatureNavigation { get; set; }
        public virtual Department Department { get; set; }
        public virtual Discharge DischargeDispositionNavigation { get; set; }
        public virtual EncounterPhysician EncounterPhysicians { get; set; }
        public virtual EncounterType EncounterType { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual Patient MrnNavigation { get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Physician Physician { get; set; }
        public virtual PlaceOfServiceOutPatient PlaceOfService { get; set; }
        public virtual PointOfOrigin PointOfOrigin { get; set; }
        public virtual ICollection<ApprovedMedicinesToAdminister> ApprovedMedicinesToAdministers { get; set; }
        public virtual ICollection<EncounterAlert> EncounterAlerts { get; set; }
        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
        public virtual ICollection<Pcarecord> Pcarecords { get; set; }
        public virtual ICollection<PhysicianAssessment> PhysicianAssessments { get; set; }
        public virtual ICollection<ProcedureReport> ProcedureReports { get; set; }
        public virtual ICollection<ProgressNote> ProgressNotes { get; set; }
    }
}
