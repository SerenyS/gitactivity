using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class AdvancedDirective
    {
        public AdvancedDirective()
        {
            PatientAdvancedDirectives = new HashSet<PatientAdvancedDirective>();
        }

        public short AdvancedDirectiveId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<PatientAdvancedDirective> PatientAdvancedDirectives { get; set; }
    }
}
