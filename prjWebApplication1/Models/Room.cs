using System;
using System.Collections.Generic;

#nullable disable

namespace PJ_MSIT143_team02.Models
{
    public partial class Room
    {
        public Room()
        {
            AdminReferences = new HashSet<AdminReference>();
            Comments = new HashSet<Comment>();
            EquipmentReferences = new HashSet<EquipmentReference>();
            ImageReferences = new HashSet<ImageReference>();
        }

        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public decimal RoomPrice { get; set; }
        public string RoomIntrodution { get; set; }
        public int MemberId { get; set; }
        public int RoomstatusId { get; set; }
        public string Address { get; set; }
        public DateTime? CreateDate { get; set; }
        public string FimagePath { get; set; }
        public int? Qty { get; set; }
        public byte[] Fimage { get; set; }

        public virtual RoomStatus Roomstatus { get; set; }
        public virtual ICollection<AdminReference> AdminReferences { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<EquipmentReference> EquipmentReferences { get; set; }
        public virtual ICollection<ImageReference> ImageReferences { get; set; }
    }
}
