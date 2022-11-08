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
        public Room room 
        { 
            get { return _room; }
            set { _room = value; }
        }

        public CRoomNew()
        {
            _room = new Room();
        }

        public int RoomId
        {
            get { return _room.RoomId; }
            set { _room.RoomId = value; }
        }
        public string RoomName
        {
            get { return _room.RoomName; }
            set { _room.RoomName = value; }
        }
        public decimal RoomPrice
        {
            get { return _room.RoomPrice; }
            set { _room.RoomPrice = value; }
        }
        public string RoomIntrodution
        {
            get { return _room.RoomIntrodution; }
            set { _room.RoomIntrodution = value; }
        }
        public int MemberId
        {
            get { return _room.MemberId; }
            set { _room.MemberId = value; }
        }
        public int RoomstatusId
        {
            get { return _room.RoomstatusId; }
            set { _room.RoomstatusId = value; }
        }
        public string Address
        {
            get { return _room.Address; }
            set { _room.Address = value; }
        }
        public DateTime? CreateDate
        {
            get { return _room.CreateDate; }
            set { _room.CreateDate = value; }
        }
        public int? Qty
        {
            get { return _room.Qty; }
            set { _room.Qty = value; }
        }


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
