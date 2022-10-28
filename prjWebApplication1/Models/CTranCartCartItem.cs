using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Models
{
    public class CTranCartCartItem
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        [DisplayName("購買量")]
        public int count { get; set; }
        [DisplayName("採購價")]
        public decimal price { get; set; }
        public decimal 小計 { get { return this.count * this.price; } }
        public Room Room { get; set; }

    }
}
