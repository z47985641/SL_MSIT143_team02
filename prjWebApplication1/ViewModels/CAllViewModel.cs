using PJ_MSIT143_team02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.ViewModel
{
    public class CAllViewModel
    {
        public Room room { get; set; }
        public Image image { get; set; }
        public ImageReference imageReference { get; set; }
        public Member member { get; set; }
        public RoomStatus RoomStatus { get; set; }
    }
}
