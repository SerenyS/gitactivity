using IS_Proj_HIT.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS_Proj_HIT.ViewModels
{
    public class ProgressNotesViewModel
    {
        
        public int ProgressNoteID { get; set; }
        public long EncounterId { get; set; }
        public int NoteTypeID { get; set; }
        public DateTime WrittenDate { get; set; }
        public string Note { get; set; }
        public int CoPhysicianID { get; set; }
        public int PhysicianId { get; set; }

        // General Constructors
        public ProgressNotesViewModel()
        { }
        public ProgressNotesViewModel(long encounterId)
        {
            this.EncounterId = encounterId;
        }
    }
}