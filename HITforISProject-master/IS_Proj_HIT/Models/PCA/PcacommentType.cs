using System;
using System.Collections.Generic;

namespace IS_Proj_HIT.Models.PCA
{
    public partial class PcaCommentType
    {
        public PcaCommentType()
        {
            PcaComment = new HashSet<PcaComment>();
        }

        public int PcaCommentTypeId { get; set; }
        public string PcaCommentTypeName { get; set; }
        public DateTime LastModified { get; set; }

        public virtual ICollection<PcaComment> PcaComment { get; set; }
    }
}
