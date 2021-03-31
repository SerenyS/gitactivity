using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class Language
    {
        public Language()
        {
            PatientLanguages = new HashSet<PatientLanguage>();
        }

        public short LanguageId { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PatientLanguage> PatientLanguages { get; set; }
    }
}
