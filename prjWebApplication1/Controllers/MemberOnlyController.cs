using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class MemberOnlyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminMainPage()
        {
            return View();
        }
        public IActionResult MemnerMainPage()
        {
            return View();
        }

        public IActionResult MemnerRoomNew()
        {
            MingSuContext db = new MingSuContext();
            CRoomNew roomnew = new CRoomNew();
            var item = from I in db.Equipment
                       select new {
                           I.EquipmentId,
                           I.EquipmentName,
                       I.EquipmentCatergoryId
                       };
            List<int> list = new List<int>();
            List<string> listname = new List<string>();
            List<int> listC = new List<int>();
            foreach (var i in item)
            {
                int count = 0;
                list.Add( i.EquipmentId);
                listname.Add( i.EquipmentName) ;
                listC.Add(i.EquipmentCatergoryId);
                count++;
            }
            roomnew.EquipmentIdlist = list;
            roomnew.EquipmentNamelist = listname;
            roomnew.EquipmentCatergoryIdlist = listC;
            return View(roomnew);
        }
        [HttpPost]
        public IActionResult MemnerRoomNew(CRoomNew NewRoom)
        {
            int count = 0;
            MingSuContext db = new MingSuContext();

            //加入房源資訊
            Room room = new Room();
            room.RoomName = NewRoom.RoomName;
            room.RoomPrice = NewRoom.RoomPrice;
            room.RoomIntrodution = NewRoom.RoomIntrodution;
            room.MemberId = (int)HttpContext.Session.GetInt32("MemberID");
            room.RoomstatusId = 4;
            room.Address = NewRoom.Address;
            room.CreateDate = DateTime.Now;
            room.Qty = NewRoom.Qty;
            int XX = NewRoom.EquipmentId.Count;
            db.Rooms.Add(room);

            //迴圈加入多圖
            var ms = new MemoryStream();
            foreach (var imgindex in 
                NewRoom.img)
            {
                count++;
                Image img = new Image();
                imgindex.CopyTo(ms);
                img.Image1 = ms.ToArray();
                db.Images.Add(img);
                db.SaveChanges();
            }

            //迴圈加入ImageReferences
            for (int i = 1; i <= count; i++)
            {
                ImageReference imgRef = new ImageReference();
                imgRef.RoomId = db.Rooms.OrderByDescending(i => i.RoomId).FirstOrDefault().RoomId;
                imgRef.ImageId = db.Images.OrderByDescending(i => i.ImageId).FirstOrDefault().ImageId -count + i;
                db.ImageReferences.Add(imgRef);
                db.SaveChanges();
            }

            //迴圈加入設備References
            foreach (var Eitem in NewRoom.EquipmentId)
            {
                EquipmentReference item = new EquipmentReference();
                item.EquipmentId = Eitem;
                item.RoomId = db.Rooms.OrderByDescending(i => i.RoomId).FirstOrDefault().RoomId;
                db.EquipmentReferences.Add(item);
                db.SaveChanges();
            }

            return RedirectToAction("TestListView", "Room");
        }
        
    }
}
