using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class PhysicianAssessment
    {
        public PhysicianAssessment()
        {
            PhysicianAssessmentDetails = new HashSet<PhysicianAssessmentDetail>();
        }

        public long PhysicianAssessmentId { get; set; }
        public long EncounterId { get; set; }
        public DateTime? PhysicianAssessmentDate { get; set; }
        public short PhysicianAssessmentTypeId { get; set; }
        public int? ReferringProvider { get; set; }
        public string ChiefComplaint { get; set; }
        public string HistoryOfPresentIllness { get; set; }
        public string SocialHistory { get; set; }
        public string FamilyHistory { get; set; }
        public string SignificantDiagnosticTests { get; set; }
        public string Assessment { get; set; }
        public string Plan { get; set; }
        public int? CoSignature { get; set; }
        public int AuthoringProvider { get; set; }
        public DateTime? WrittenDateTime { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int EditedBy { get; set; }

        public virtual Physician AuthoringProviderNavigation { get; set; }
        public virtual Physician CoSignatureNavigation { get; set; }
        public virtual UserTable EditedByNavigation { get; set; }
        public virtual Encounter Encounter { get; set; }
        public virtual PhysicianAssessmentType PhysicianAssessmentType { get; set; }
        public virtual Physician ReferringProviderNavigation { get; set; }
        public virtual ICollection<PhysicianAssessmentDetail> PhysicianAssessmentDetails { get; set; }
    }
}
