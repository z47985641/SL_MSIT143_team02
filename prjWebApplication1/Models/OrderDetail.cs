using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class OrderDetail
    {
        public int Id { get; set; }
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int RoomId { get; set; }
        public decimal OrderPrice { get; set; }
        public int? RoomDiscountId { get; set; }
        public DateTime OrderStartDate { get; set; }
        public DateTime OrderEndDate { get; set; }
        public int PayId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Payment Pay { get; set; }
        public virtual Discount RoomDiscount { get; set; }
    }
}
