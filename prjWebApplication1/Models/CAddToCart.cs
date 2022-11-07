using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Models
{
    public class CAddToCart
    {

            public int RoomId { get; set; }
            public string RoomName { get; set; }
            public decimal RoomPrice { get; set; }
            public int Qty { get; set; }
            public int? count { get; set; }
            public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public decimal? ActivityPrice { get; set; }





    }
}
