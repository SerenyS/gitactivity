using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class OrderInfo
    {
        public OrderInfo()
        {
            ApprovedMedicinesToAdministers = new HashSet<ApprovedMedicinesToAdminister>();
        }

        public long OrderInfoId { get; set; }
        public long EncounterId { get; set; }
        public int OrderTypeId { get; set; }
        public int OrderingAuthor { get; set; }
        public DateTime OrderDate { get; set; }
        public int PriorityId { get; set; }
        public int? CoAuthorId { get; set; }
        public bool? CoAuthorApproved { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string Notes { get; set; }

        public virtual Physician CoAuthor { get; set; }
        public virtual Encounter Encounter { get; set; }
        public virtual OrderType OrderType { get; set; }
        public virtual Physician OrderingAuthorNavigation { get; set; }
        public virtual Priority Priority { get; set; }
        public virtual ICollection<ApprovedMedicinesToAdminister> ApprovedMedicinesToAdministers { get; set; }
    }
}
