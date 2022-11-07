using PJ_MSIT143_team02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.ViewModels
{
    public class COrderdetailViewModel
    {
        public IEnumerable<Order> order { get; set; }
        public IEnumerable<OrderDetail> orderdetail { get; set; }
        public int OrderId { get; set; }
        public string OrderRemark { get; set; }
        public int MemberId { get; set; }
        public int OrderstatusId { get; set; }
        public int? RoomId { get; set; }
        public int? ActivityId { get; set; }

        public int OrderDetailId { get; set; }
        public decimal OrderPrice { get; set; }
        public int? RoomDiscountId { get; set; }
        public DateTime OrderStartDate { get; set; }
        public DateTime OrderEndDate { get; set; }
        public string Payment { get; set; }


        public string ActivityName { get; set; }
        public DateTime? ActivityDate { get; set; }
        public string ActivityInfo { get; set; }
        public int ActivityCapacity { get; set; }
        public string ActivityStatus { get; set; }
        public string ActivityLocation { get; set; }
        public decimal ActivityPrice { get; set; }

        public string RoomName { get; set; }
        public decimal RoomPrice { get; set; }
        public string RoomIntrodution { get; set; }
        public int RoomstatusId { get; set; }
        public string Address { get; set; }
        public DateTime? CreateDate { get; set; }
        public string FimagePath { get; set; }
        public int? Qty { get; set; }
        public byte[] Fimage { get; set; }

    }
}
