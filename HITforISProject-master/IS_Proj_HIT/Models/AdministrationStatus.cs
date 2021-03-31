using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class AdministrationStatus
    {
        public AdministrationStatus()
        {
            Emars = new HashSet<Emar>();
        }

        public int AdministrationStatusId { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Emar> Emars { get; set; }
    }
}
