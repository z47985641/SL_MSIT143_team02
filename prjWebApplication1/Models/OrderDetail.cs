using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public decimal OrderPrice { get; set; }
        public int? RoomDiscountId { get; set; }
        public DateTime OrderStartDate { get; set; }
        public DateTime OrderEndDate { get; set; }
        public string Payment { get; set; }
        public int? RoomId { get; set; }
        public int? ActivityId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Discount RoomDiscount { get; set; }
    }
}
