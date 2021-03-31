using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Physician
    {
        public Physician()
        {
            EmarAdministeredByNavigations = new HashSet<Emar>();
            EmarAssignedAdministratorNavigations = new HashSet<Emar>();
            EncounterAuthoringProviderNavigations = new HashSet<Encounter>();
            EncounterCoSignatureNavigations = new HashSet<Encounter>();
            EncounterPhysicians = new HashSet<Encounter>();
            EncounterPhysiciansNavigation = new HashSet<EncounterPhysician>();
            OrderInfoCoAuthors = new HashSet<OrderInfo>();
            OrderInfoOrderingAuthorNavigations = new HashSet<OrderInfo>();
            PhysicianAssessmentAuthoringProviderNavigations = new HashSet<PhysicianAssessment>();
            PhysicianAssessmentCoSignatureNavigations = new HashSet<PhysicianAssessment>();
            PhysicianAssessmentReferringProviderNavigations = new HashSet<PhysicianAssessment>();
            ProcedureReportAuthoringProviderNavigations = new HashSet<ProcedureReport>();
            ProcedureReportCoSignatureNavigations = new HashSet<ProcedureReport>();
            ProcedureReportPhysicians = new HashSet<ProcedureReportPhysician>();
            ProgressNoteCoPhysicians = new HashSet<ProgressNote>();
            ProgressNotePhysicians = new HashSet<ProgressNote>();
        }

        public int PhysicianId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Credentials { get; set; }
        public string License { get; set; }
        public int? AddressId { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public int? SpecialtyId { get; set; }
        public int? ProviderTypeId { get; set; }
        public byte? ProviderStatus { get; set; }
        public DateTime LastModified { get; set; }
        public short? Pin { get; set; }

        public virtual Address Address { get; set; }
        public virtual ProviderType ProviderType { get; set; }
        public virtual Specialty Specialty { get; set; }
        public virtual ICollection<Emar> EmarAdministeredByNavigations { get; set; }
        public virtual ICollection<Emar> EmarAssignedAdministratorNavigations { get; set; }
        public virtual ICollection<Encounter> EncounterAuthoringProviderNavigations { get; set; }
        public virtual ICollection<Encounter> EncounterCoSignatureNavigations { get; set; }
        public virtual ICollection<Encounter> EncounterPhysicians { get; set; }
        public virtual ICollection<EncounterPhysician> EncounterPhysiciansNavigation { get; set; }
        public virtual ICollection<OrderInfo> OrderInfoCoAuthors { get; set; }
        public virtual ICollection<OrderInfo> OrderInfoOrderingAuthorNavigations { get; set; }
        public virtual ICollection<PhysicianAssessment> PhysicianAssessmentAuthoringProviderNavigations { get; set; }
        public virtual ICollection<PhysicianAssessment> PhysicianAssessmentCoSignatureNavigations { get; set; }
        public virtual ICollection<PhysicianAssessment> PhysicianAssessmentReferringProviderNavigations { get; set; }
        public virtual ICollection<ProcedureReport> ProcedureReportAuthoringProviderNavigations { get; set; }
        public virtual ICollection<ProcedureReport> ProcedureReportCoSignatureNavigations { get; set; }
        public virtual ICollection<ProcedureReportPhysician> ProcedureReportPhysicians { get; set; }
        public virtual ICollection<ProgressNote> ProgressNoteCoPhysicians { get; set; }
        public virtual ICollection<ProgressNote> ProgressNotePhysicians { get; set; }
    }
}
