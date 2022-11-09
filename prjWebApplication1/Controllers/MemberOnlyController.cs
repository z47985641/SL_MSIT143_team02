using Microsoft.AspNetCore.Hosting;
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
        private readonly IWebHostEnvironment _hostinEnvironment;
        public MemberOnlyController(IWebHostEnvironment hostinEnvironment)
        {
            _hostinEnvironment = hostinEnvironment;
        }
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
            var eId = db.Equipment.Select(e => e.EquipmentId);
            var eName = db.Equipment.Select(e => e.EquipmentName);
            var eCate = db.Equipment.Select(e => e.EquipmentCatergoryId);
           
            roomnew.EquipmentIdlist = eId.ToList();
            roomnew.EquipmentNamelist = eName.ToList(); 
            roomnew.EquipmentCatergoryIdlist = eCate.ToList();
            return View(roomnew);
        }
        [HttpPost]
        public IActionResult MemnerRoomNew(CRoomNew NewRoom)
        {
            int count = 0;
            MingSuContext db = new MingSuContext();

            //加入房源資訊
            db.Rooms.Add(NewRoom.room);

            //迴圈加入多圖
            foreach (var imgindex in NewRoom.img)
            {
                count++;
                Image img = new Image();
                using(var ms1 = new MemoryStream())
                {
                    imgindex.CopyTo(ms1);
                    img.Image1 = ms1.ToArray();
                }
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

            return RedirectToAction("MemnerRoomList");
        }
        public IActionResult MemnerRoomList()
        {
            if (HttpContext.Session.GetInt32("MemberID") == null)
                return RedirectToAction("Login", "MemberCreate");

            MingSuContext db = new MingSuContext();
            CRoomNew roomItem = new CRoomNew();
            roomItem.rooms = db.Rooms.Where(r => r.MemberId == HttpContext.Session.GetInt32("MemberID") && r.RoomstatusId != 5).Select(r => r).ToList(); ;
            return View(roomItem);
        }
        public IActionResult MemnerRoomEdit(int? roomId)
        {
            MingSuContext db = new MingSuContext();
            CRoomNew roomNew = new CRoomNew();

            //加入房間資料
            var roomData = db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            roomNew.room = roomData;

            //加入設備選項
            var eId = db.Equipment.Select(e => e.EquipmentId);
            var eName = db.Equipment.Select(e => e.EquipmentName);
            var eCate = db.Equipment.Select(e => e.EquipmentCatergoryId);
            roomNew.EquipmentIdlist = eId.ToList();
            roomNew.EquipmentNamelist = eName.ToList();
            roomNew.EquipmentCatergoryIdlist = eCate.ToList();

            //帶入設備狀態
            List<int> idlist = new List<int>();
            var EE = db.EquipmentReferences.Where(e => e.RoomId == roomId).Select(e => e.EquipmentId);
            roomNew.EquipmentId = EE.ToList();
            db.SaveChanges();


            //加入圖片
            var imgedit = db.ImageReferences.Where(i => i.RoomId == roomId).Select(i=>i.ImageId);
            roomNew.imgId = imgedit.ToList();

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
            }
            if (roomedit.img != null)
            {
                foreach (var imgindex in roomedit.img)
                {
                    count++;
                    Image img = new Image();
                    using (var ms1 = new MemoryStream())
                    {
                        imgindex.CopyTo(ms1);
                        img.Image1 = ms1.ToArray();
                    }
                    db.Images.Add(img);
                    db.SaveChanges();
                }
                //迴圈加入ImageReferences
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
            COrderdetailViewModel vb = new COrderdetailViewModel();
            IEnumerable<COrderdetailViewModel> order = (from o in db.Orders
                                       where o.MemberId == HttpContext.Session.GetInt32("MemberID")
                                       join m in db.OrderDetails on o.OrderId equals m.OrderId
                                       join s in db.OrderStatuses on o.OrderstatusId equals s.OrderstatusId
                                       join r in db.Rooms on o.RoomId equals r.RoomId
                                                        select new COrderdetailViewModel()
                                       { OrderId= o.OrderId,
                                        OrderRemark=o.OrderRemark,
                                        MemberId = o.MemberId,
                                        OrderstatusId = o.OrderstatusId,
                                        RoomId = o.RoomId,
                                        ActivityId = o.ActivityId,
                                        OrderPrice =m.OrderPrice,
                                        OrderStartDate =m.OrderStartDate,
                                        OrderEndDate = m.OrderEndDate,
                                        Payment = m.Payment,
                                        Qty =m.Qty,
                                           OrderstatusContent = s.OrderstatusContent,
                                           RoomName = r.RoomName,

                                        
                                       }).ToList();
            
            return View(order);
        }
       

        }
}
