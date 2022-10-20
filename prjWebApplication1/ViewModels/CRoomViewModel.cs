using Microsoft.AspNetCore.Http;
using PJ_MSIT143_team02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.ViewModel
{
    public class CRoomViewModel
    {
        private Room _Room;
        public Room room
        {
            get { return _Room; }
            set { _Room = value; }
        }
        public CRoomViewModel()
        {
            _Room = new Room();
        }
        public int RoomId
        {
            get { return _Room.RoomId; }
            set { _Room.RoomId = value; }
        }
        public string RoomName
        {
            get { return _Room.RoomName; }
            set { _Room.RoomName = value; }
        }
        public decimal RoomPrice
        {
            get { return _Room.RoomPrice; }
            set { _Room.RoomPrice = value; }
        }
        public string RoomIntrodution
        {
            get { return _Room.RoomIntrodution; }
            set { _Room.RoomIntrodution = value; }
        }
        public int MemberId
        {
            get { return _Room.MemberId; }
            set { _Room.MemberId = value; }
        }
        public int RoomstatusId
        {
            get { return _Room.RoomstatusId; }
            set { _Room.RoomstatusId = value; }
        }
        public string Address
        {
            get { return _Room.Address; }
            set { _Room.Address = value; }
        }
        public DateTime? CreateDate
        {
            get { return _Room.CreateDate; }
            set { _Room.CreateDate = value; }
        }
        public int Qty
        {
            get { return _Room.Qty; }
            set { _Room.Qty = value; }
        }
        public IFormFile photo { get; set; }
    }
}
