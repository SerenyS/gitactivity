using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using IS_Proj_HIT.Models;

namespace IS_Proj_HIT.ViewModels
{
    public class ViewDischargePageModel
    {
        public Encounter Encounter { get; set; }
        public Patient Patient { get; set; }
    }
}