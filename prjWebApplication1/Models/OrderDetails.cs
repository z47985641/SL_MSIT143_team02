using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class OrderDetails
    {
        public int OrderDetailId { get; set; }
        public int OrderId { get; set; }
        public int RoomId { get; set; }
        public decimal OrderPrice { get; set; }
        public int? RoomDiscountId { get; set; }
        public DateTime OrderStartDate { get; set; }
        public DateTime OrderEndDate { get; set; }
        public string Payment { get; set; }

        public virtual Order Order { get; set; }
        public virtual Discount RoomDiscount { get; set; }
    }
}
