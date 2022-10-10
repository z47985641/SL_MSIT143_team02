using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class RoomStatus
    {
        public RoomStatus()
        {
            Rooms = new HashSet<Room>();
        }

        public int RoomstatusId { get; set; }
        public string StatusContent { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }
    }
}
