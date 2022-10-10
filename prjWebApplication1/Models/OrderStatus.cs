using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class OrderStatus
    {
        public OrderStatus()
        {
            Orders = new HashSet<Order>();
        }

        public int OrderstatusId { get; set; }
        public string OrderstatusContent { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
