using System;

namespace IS_Proj_HIT.Models.PCA
{
    public partial class PcaComment
    {
        public int PcaCommentId { get; set; }
        public int PcaId { get; set; }
        public int PcaCommentTypeId { get; set; }
        public string Comment { get; set; }
        public DateTime? DateCommentAdded { get; set; }
        public DateTime LastModified { get; set; }

        public virtual PcaRecord Pca { get; set; }
        public virtual PcaCommentType PcacommentType { get; set; }
    }
}
