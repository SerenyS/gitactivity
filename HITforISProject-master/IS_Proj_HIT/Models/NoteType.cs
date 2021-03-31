using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class NoteType
    {
        public NoteType()
        {
            ProgressNotes = new HashSet<ProgressNote>();
        }

        public int NoteTypeId { get; set; }
        public string NoteType1 { get; set; }
        public DateTime? LastModified { get; set; }

        public virtual ICollection<ProgressNote> ProgressNotes { get; set; }
    }
}
