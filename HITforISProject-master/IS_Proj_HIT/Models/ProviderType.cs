using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class ProviderType
    {
        public ProviderType()
        {
            Physicians = new HashSet<Physician>();
        }

        public int ProviderTypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime LastModified { get; set; }
        public bool? CosignRequired { get; set; }

        public virtual ICollection<Physician> Physicians { get; set; }
    }
}
