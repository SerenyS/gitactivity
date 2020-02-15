using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using IS_Proj_HIT.Models;

namespace IS_Proj_HIT.ViewModels
{
    public class ViewEncounterPageModel
    {
        public Encounter Encounter { get; set; }
        public Patient Patient { get; set; }
    }
}