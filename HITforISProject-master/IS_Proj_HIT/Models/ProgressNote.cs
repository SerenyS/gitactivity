using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class ProgressNote
    {
        public long ProgressNoteId { get; set; }
        public long EncounterId { get; set; }
        public int NoteTypeId { get; set; }
        public DateTime? WrittenDate { get; set; }
        public string Note { get; set; }
        public int? CoPhysicianId { get; set; }
        public int PhysicianId { get; set; }
        public DateTime? LastUpdated { get; set; }
        public int EditedBy { get; set; }

        public virtual Physician CoPhysician { get; set; }
        public virtual UserTable EditedByNavigation { get; set; }
        public virtual Encounter Encounter { get; set; }
        public virtual NoteType NoteType { get; set; }
        public virtual Physician Physician { get; set; }
    }
}
