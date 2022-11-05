using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PJ_MSIT143_team02.Models;

namespace PJ_MSIT143_team02.ViewModels
{
    public class CKeywordViewModel
    {
        public string txtKeyword { get; set; }
        public DateTime mydatein { get; set; }
        public DateTime mydateout { get; set; }
        public int txtQty { get; set; }
        //public int EquipmentId { get; set; }
        //public int RoomId { get; set; }


        public IEnumerable<Equipment> Equipment { get; set; }   //EquipmentID
        public IEnumerable<EquipmentReference> EquipmentReference { get; set; }  //EquipmentID RoomID
        public IEnumerable<Room> Room { get; set; }  //RoomID







    }
}
