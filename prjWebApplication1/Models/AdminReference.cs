using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class AdminReference
    {
        public int AdminId { get; set; }
        public int RoomId { get; set; }
        public int AdminReferenceId { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual Room Room { get; set; }
    }
}
