using PJ_MSIT143_team02.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.ViewModels
{
    public class CRoomMemberViewModel
    {
        public List<房源及會員> 房源及會員;
    }
    public class 房源及會員
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        [DisplayName("購買量")]
        public int count { get; set; }
        [DisplayName("採購價")]
        public decimal price { get; set; }
        public decimal 小計 { get { return this.count * this.price; } }
        public Room Room { get; set; }
        public int MemberId { get; set; }
        public string MemberAccount { get; set; }

        public string MemberName { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPhone { get; set; }
        public Member member { get; set; }
        public int OrderId { get; set; }
        public string OrderRemark { get; set; }
        public int OrderstatusId { get; set; }
        public Order order { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }

        //public DateTime? ActivityDate { get; set; }
        public int? ActivityCapacity { get; set; }

        //public string ActivityLocation { get; set; }



    }
}
