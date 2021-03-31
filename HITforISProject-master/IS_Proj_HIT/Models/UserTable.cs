using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class UserTable
    {
        public UserTable()
        {
            Encounters = new HashSet<Encounter>();
            InverseInstructor = new HashSet<UserTable>();
            PhysicianAssessments = new HashSet<PhysicianAssessment>();
            ProcedureReports = new HashSet<ProcedureReport>();
            ProgressNotes = new HashSet<ProgressNote>();
            UserFacilities = new HashSet<UserFacility>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProgramEnrolledIn { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime LastModified { get; set; }
        public string AspNetUsersId { get; set; }
        public int? InstructorId { get; set; }

        public virtual AspNetUser AspNetUsers { get; set; }
        public virtual UserTable Instructor { get; set; }
        public virtual ICollection<Encounter> Encounters { get; set; }
        public virtual ICollection<UserTable> InverseInstructor { get; set; }
        public virtual ICollection<PhysicianAssessment> PhysicianAssessments { get; set; }
        public virtual ICollection<ProcedureReport> ProcedureReports { get; set; }
        public virtual ICollection<ProgressNote> ProgressNotes { get; set; }
        public virtual ICollection<UserFacility> UserFacilities { get; set; }
    }
}
