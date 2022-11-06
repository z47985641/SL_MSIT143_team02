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
            db.Rooms.Add(room);

            //迴圈加入多圖
            var ms = new MemoryStream();
            foreach (var imgindex in NewRoom.img)
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
        public IActionResult MemnerRoomList()
        {
            if (HttpContext.Session.GetInt32("MemberID") == null)
                return RedirectToAction("Login", "MemberCreate");

            MingSuContext db = new MingSuContext();
            var roomItem = from r in db.Rooms
                           where r.MemberId == HttpContext.Session.GetInt32("MemberID")&& r.RoomstatusId != 5
                           select r;
            return View(roomItem);
        }
        public IActionResult MemnerRoomEdit(int? roomId)
        {
            MingSuContext db = new MingSuContext();
            CRoomNew roomNew = new CRoomNew();
            //加入房間資料
            var roomData = db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            roomNew.RoomId = roomData.RoomId;
                roomNew.RoomName = roomData.RoomName;
                roomNew.RoomPrice = roomData.RoomPrice;
                roomNew.RoomIntrodution = roomData.RoomIntrodution;
                roomNew.MemberId = roomData.MemberId;
                roomNew.RoomstatusId = roomData.RoomstatusId;
                roomNew.Address = roomData.Address;
                roomNew.CreateDate = roomData.CreateDate;
                roomNew.Qty = roomData.Qty;


            //加入設備選項
            List<int> list = new List<int>();
            List<string> listname = new List<string>();
            List<int> listC = new List<int>();

            foreach (var i in db.Equipment)
            {
                list.Add(i.EquipmentId);
                listname.Add(i.EquipmentName);
                listC.Add(i.EquipmentCatergoryId);
            }

            roomNew.EquipmentIdlist = list;
            roomNew.EquipmentNamelist = listname;
            roomNew.EquipmentCatergoryIdlist = listC;

            //帶入設備狀態
            List<int> idlist = new List<int>();
            var EquipmentRE = from e in db.EquipmentReferences
                              where e.RoomId == roomId
                              select e;
            foreach(var e in EquipmentRE)
            {
                idlist.Add(e.EquipmentId);
                db.EquipmentReferences.Remove(e);
            }

            roomNew.EquipmentId = idlist;
            db.SaveChanges();


            //加入圖片
            var imgedit = from i in db.ImageReferences
                          where i.RoomId == roomId
                          select i.ImageId;
            List<int> IDlist = new List<int>();
            IDlist = imgedit.ToList();
            roomNew.imgId = IDlist;

            return View(roomNew);
        }
        [HttpPost]
        public IActionResult MemnerRoomEdit(CRoomNew roomedit)
        {

            int count = 0;
            MingSuContext db = new MingSuContext();

            //加入房源資訊
            var roomData = db.Rooms.FirstOrDefault(r => r.RoomId == roomedit.RoomId);

            roomData.RoomName = roomedit.RoomName;
            roomData.RoomPrice = roomedit.RoomPrice;
            roomData.RoomIntrodution = roomedit.RoomIntrodution;
            roomData.RoomstatusId = roomedit.RoomstatusId;
            roomData.Address = roomedit.Address;
            roomData.Qty = roomedit.Qty;

            //迴圈加入設備References
            foreach (var Eitem in roomedit.EquipmentId)
            {
                EquipmentReference item = new EquipmentReference();
                item.EquipmentId = Eitem;
                item.RoomId = roomedit.RoomId;
                db.EquipmentReferences.Add(item);
                db.SaveChanges();
            }
            if (roomedit.img != null)
            {
            var ms = new MemoryStream();
            foreach (var imgindex in roomedit.img)
            {
                Image img = new Image();
                imgindex.CopyTo(ms);
                img.Image1 = ms.ToArray();
                db.Images.Add(img);
                count++;
            }//迴圈加入ImageReferences
            for (int i = 1; i <= roomedit.img.Count; i++)
            {
                ImageReference imgRef = new ImageReference();
                imgRef.RoomId = roomedit.RoomId;
                imgRef.ImageId = db.Images.OrderByDescending(i => i.ImageId).FirstOrDefault().ImageId - count + i;
                db.ImageReferences.Add(imgRef);

            }
            }
            

            

            db.SaveChanges();
            return RedirectToAction("MemnerRoomList");
        }
            public IActionResult MemnerRoomDelete(int? roomId)
        {
            MingSuContext db = new MingSuContext();
            var v = db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            v.RoomstatusId = 5;
            db.SaveChanges();

            return RedirectToAction("MemnerRoomList");
        }
        public IActionResult MemnerRoomPhotoDelete(int? ImageId)
        {
            MingSuContext db = new MingSuContext();
            ImageReference imgRe = db.ImageReferences.FirstOrDefault(img => img.ImageId == ImageId);
            Image img =  db.Images.FirstOrDefault(img => img.ImageId == ImageId);
            db.ImageReferences.Remove(imgRe);
            db.SaveChanges();
            //db.Images.Remove(img);
            //db.SaveChanges();

            return NoContent();
        }

        public FileResult ShowPhoto(int imgId)
        { 
            MingSuContext db = new MingSuContext();
            Image img = db.Images.FirstOrDefault(img =>img.ImageId == imgId);
            byte[] content =img.Image1;
            return File(content, "image/jpeg");
        }

        public IActionResult OrderDetailList()
        {
            MingSuContext db = new MingSuContext();
            IEnumerable<OrderDetail> orderDetail = null;
            IEnumerable<Order> order = (from o in db.Orders
                                       where o.MemberId == HttpContext.Session.GetInt32("MemberID")
                                       select o).ToList();
            foreach (var item in order)
            {
                orderDetail = from o in db.OrderDetails
                              where o.OrderId == item.OrderId
                              select o;
            }
            COrderdetailViewModel vb = new COrderdetailViewModel();
            vb.order = order;
            vb.orderdetail = orderDetail;

            return View(vb);
        }
       

        }
}
