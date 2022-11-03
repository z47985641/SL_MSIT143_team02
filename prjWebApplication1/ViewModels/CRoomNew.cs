using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.ViewModels
{
    public partial class CRoomNew
    {
        private Room _room;
        private Equipment _equipment;

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


        public int ImageId { get; set; }
        public byte[] Image1 { get; set; }
        public string ImageCaption { get; set; }

        public List<IFormFile> img { get; set; }
        public List<int> imgId { get; set; }


        public List<int> EquipmentIdlist { get; set; }
        public List<int> EquipmentCatergoryIdlist { get; set; }
        public List<string> EquipmentNamelist { get; set; }
        public int EquipmentCatergoryId { get; set; }

        public List<int> EquipmentId { get; set; }
        
        public string EquipmentName { get; set; }

        //public int EquipmentId
        //{
        //    get { return _equipment.EquipmentId; }
        //    set { _equipment.EquipmentId = value; }
        //}
        //public string EquipmentName
        //{
        //    get { return _equipment.EquipmentName; }
        //    set { _equipment.EquipmentName = value; }
        //}

    }
}
