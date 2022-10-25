using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.ViewModels
{
    public class CLikelist
    {
        public int OrderId { get; set; }
        public byte[] Image { get; set; }
        public int ImageID { get; set; }
        public int RoomID { get; set; }
        public string RoomName { get; set; }
        public decimal RoomPrice { get; set; }
        public string RoomIntrodution { get; set; }
        public string Address { get; set; }
        public int Qty { get; set; }
    }
}
