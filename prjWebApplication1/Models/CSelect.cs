using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Models
{
    public class CSelect
    {
        public int EquipmentId { get; set; }
        public string EquipmentName { get; set; }
        public int EquipmentCatergoryId { get; set; }
        public int RoomId { get; set; }
        public int EquipmentReferenceId { get; set; }
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
        public Room room { get; set; }

    }
}
