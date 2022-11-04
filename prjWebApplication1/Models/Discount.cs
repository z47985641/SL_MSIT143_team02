using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class Discount
    {
        public Discount()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int RoomDiscountId { get; set; }
        public string DiscountName { get; set; }
        public string DiscountInfo { get; set; }
        public decimal? DiscountValue { get; set; }
        public string Coupon { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
