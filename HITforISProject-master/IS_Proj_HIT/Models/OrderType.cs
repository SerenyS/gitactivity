using System;
using System.Collections.Generic;

#nullable disable

namespace IS_Proj_HIT.Models
{
    public partial class OrderType
    {
        public OrderType()
        {
            OrderInfos = new HashSet<OrderInfo>();
        }

        public int OrderTypeId { get; set; }
        public string OrderName { get; set; }
        public string Description { get; set; }

        public virtual ICollection<OrderInfo> OrderInfos { get; set; }
    }
}
