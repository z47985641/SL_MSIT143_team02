using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModel;
using PJ_MSIT143_team02.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace PJ_MSIT143_team02.Controllers
{

    public class RoomController : Controller
    {

        MingSuContext db = new MingSuContext();

        private IWebHostEnvironment _enviro;
        public RoomController(IWebHostEnvironment p)
        {
            _enviro = p;
        }


        public IActionResult List(CKeywordViewModel model)
        {
            MingSuContext db = new MingSuContext();
            CRoomNew roomItem = new CRoomNew();
            roomItem.rooms = db.Rooms.Select(r => r).ToList(); ;
            return View(roomItem);
        }
        public IActionResult TestListView(/*CKeywordViewModel model*/int number)
        {
            DateTime thisDate = new DateTime(0001, 1, 1);
            MingSuContext db = new MingSuContext();

            CKeywordViewModel ck = new CKeywordViewModel();
            var Equipment = db.Equipment;
            var EquipmentReference = db.EquipmentReferences;
            var Room = db.Rooms;
            ck.Equipment = Equipment;  //EquipmentID
            ck.EquipmentReference = EquipmentReference;  //EquipmentID RoomID
            ck.Room = Room; //RoomID

            IEnumerable<EquipmentReference> eqr = null;
            if (string.IsNullOrEmpty(ck.txtKeyword))
                eqr = (from r in db.EquipmentReferences
                       join i in db.Equipment on r.EquipmentId equals i.EquipmentId
                       join k in db.Rooms on r.RoomId equals k.RoomId
                       select r).ToList();

            IEnumerable<Room> datas = null;
            if (string.IsNullOrEmpty(ck.txtKeyword))
            {
                if (0.Equals(ck.txtQty)
                    && (thisDate.Equals(ck.mydatein)
                    && thisDate.Equals(ck.mydateout)))
                    datas = (from r in db.Rooms
                             select r).ToList();
                else if (thisDate.Equals(ck.mydatein)
                    && thisDate.Equals(ck.mydateout))
                    datas = (from r in db.Rooms
                             where r.Qty.Equals(ck.txtQty)
                             select r).ToList();
                else if (0.Equals(ck.txtQty) && thisDate.Equals(ck.mydateout))
                    datas = (from r in db.Rooms
                             join o in db.OrderDetails on r.RoomId equals o.RoomId
                             into subGrp
                             from s in subGrp.DefaultIfEmpty()
                             where s.OrderStartDate != ck.mydatein
                             select r).ToList();
                else if (0.Equals(ck.txtQty) && thisDate.Equals(ck.mydatein))
                    datas = (from r in db.Rooms
                             join o in db.OrderDetails on r.RoomId equals o.RoomId
                             into subGrp
                             from s in subGrp.DefaultIfEmpty()
                             where s.OrderEndDate != ck.mydateout
                             select r).ToList();
                else if (0.Equals(ck.txtQty))
                    datas = (from r in db.Rooms
                             join o in db.OrderDetails on r.RoomId equals o.RoomId
                             into subGrp
                             from s in subGrp.DefaultIfEmpty()
                             where s.OrderStartDate != ck.mydatein
                             && s.OrderEndDate != ck.mydateout
                             select r).ToList();
            }
            else
            {
                if (0.Equals(ck.txtQty)
                    && thisDate.Equals(ck.mydatein)
                    && thisDate.Equals(ck.mydateout))
                    datas = from r in db.Rooms
                            where r.RoomName.Contains(ck.txtKeyword)
                            || r.RoomPrice.ToString().Contains(ck.txtKeyword)
                            || r.RoomIntrodution.Contains(ck.txtKeyword)
                            || r.MemberId.ToString().Contains(ck.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(ck.txtKeyword)
                            || r.Address.Contains(ck.txtKeyword)
                            select r;
                else if (thisDate.Equals(ck.mydatein)
                    && thisDate.Equals(ck.mydateout))
                    datas = from r in db.Rooms
                            where r.RoomName.Contains(ck.txtKeyword)
                            || r.RoomPrice.ToString().Contains(ck.txtKeyword)
                            || r.RoomIntrodution.Contains(ck.txtKeyword)
                            || r.MemberId.ToString().Contains(ck.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(ck.txtKeyword)
                            || r.Address.Contains(ck.txtKeyword)
                            || r.Qty.Equals(ck.txtQty)
                            select r;
                else if (0.Equals(ck.txtQty) && thisDate.Equals(ck.mydateout))
                    datas = from r in db.Rooms
                            join o in db.OrderDetails on r.RoomId equals o.RoomId
                            into subGrp
                            from s in subGrp.DefaultIfEmpty()
                            where r.RoomName.Contains(ck.txtKeyword)
                            || r.RoomPrice.ToString().Contains(ck.txtKeyword)
                            || r.RoomIntrodution.Contains(ck.txtKeyword)
                            || r.MemberId.ToString().Contains(ck.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(ck.txtKeyword)
                            || r.Address.Contains(ck.txtKeyword)
                            || s.OrderEndDate != ck.mydatein
                            select r;
                else if (0.Equals(ck.txtQty) && thisDate.Equals(ck.mydatein))
                    datas = from r in db.Rooms
                            join o in db.OrderDetails on r.RoomId equals o.RoomId
                            into subGrp
                            from s in subGrp.DefaultIfEmpty()
                            where r.RoomName.Contains(ck.txtKeyword)
                            || r.RoomPrice.ToString().Contains(ck.txtKeyword)
                            || r.RoomIntrodution.Contains(ck.txtKeyword)
                            || r.MemberId.ToString().Contains(ck.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(ck.txtKeyword)
                            || r.Address.Contains(ck.txtKeyword)
                            || s.OrderEndDate != ck.mydateout
                            select r;
                else if (0.Equals(ck.txtQty))
                    datas = from r in db.Rooms
                            join o in db.OrderDetails on r.RoomId equals o.RoomId
                            into subGrp
                            from s in subGrp.DefaultIfEmpty()
                            where r.RoomName.Contains(ck.txtKeyword)
                            || r.RoomPrice.ToString().Contains(ck.txtKeyword)
                            || r.RoomIntrodution.Contains(ck.txtKeyword)
                            || r.MemberId.ToString().Contains(ck.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(ck.txtKeyword)
                            || r.Address.Contains(ck.txtKeyword)
                            || (s.OrderStartDate != ck.mydatein
                            && s.OrderEndDate != ck.mydateout)
                            select r;
                else
                    datas = from r in db.Rooms
                            join o in db.OrderDetails on r.RoomId equals o.RoomId
                            into subGrp
                            from s in subGrp.DefaultIfEmpty()
                            where r.RoomName.Contains(ck.txtKeyword)
                            || r.RoomPrice.ToString().Contains(ck.txtKeyword)
                            || r.RoomIntrodution.Contains(ck.txtKeyword)
                            || r.MemberId.ToString().Contains(ck.txtKeyword)
                            || r.RoomstatusId.ToString().Contains(ck.txtKeyword)
                            || r.Address.Contains(ck.txtKeyword)
                            || r.Qty.Equals(ck.txtQty)
                            || s.OrderStartDate != ck.mydatein
                            || s.OrderEndDate != ck.mydateout
                            select r;
            }

            //return View("TestListView", datas.Where(e => e.RoomstatusId != 5));
            return View("TestListView", ck);

        }
        [HttpGet]
        public ActionResult forEquipment(string Equipment)
        {
            int EquipmentID = 0;
            EquipmentID = int.Parse(Equipment);

            var q = db.EquipmentReferences.Where(i => i.EquipmentId == EquipmentID).Select(i => i.Room).ToList();
            string jsonString = JsonSerializer.Serialize(q);

            return Json(q);

        }




        public IActionResult AddRoom(CKeywordViewModel model)
        {
            return View();

        }


        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Room p)
        {
            MingSuContext db = new MingSuContext();
            db.Rooms.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        public IActionResult Delete(int? roomId)
        {
            MingSuContext db = new MingSuContext();
            var v = db.Rooms.FirstOrDefault(r => r.RoomId == roomId);
            v.RoomstatusId = 5;
            db.SaveChanges();

            return RedirectToAction("List");
        }
        public ActionResult Edit(int? roomId)
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
            var imgedit = db.ImageReferences.Where(i => i.RoomId == roomId).Select(i => i.ImageId);
            roomNew.imgId = imgedit.ToList();

            return View(roomNew);
        }
        [HttpPost]
        public IActionResult Edit(CRoomNew roomedit)
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
            return RedirectToAction("List");
        }
        public IActionResult Details(int? Id)
        {
            if (Id != null)
            {
                MingSuContext db = new MingSuContext();
                IEnumerable<Room> r = db.Rooms.Where(r => r.RoomId == Id);
                if (r != null)
                    return View(r.ToList());
            }
            return RedirectPermanent("TestListView");
        }
        public FileResult ShowPhoto(int roomId)
        {
            byte[] content;
            MingSuContext db = new MingSuContext();
            ImageReference imgref = db.ImageReferences.FirstOrDefault(img => img.RoomId == roomId);
            if (imgref == null)
            {
                Image img = db.Images.FirstOrDefault(img => img.ImageId == 102);
                content = img.Image1;
            }
            else
            {
                Image img = db.Images.FirstOrDefault(img => img.ImageId == imgref.ImageId);
                content = img.Image1;
            }

            return File(content, "image/jpeg");
        }
        //public static byte[] GetBytesFromImage(string filename)
        //{
        //    try
        //    {
        //        FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
        //        int length = (int)fs.Length;
        //        byte[] image = new byte[length];
        //        fs.Read(image, 0, length);
        //        fs.Close();
        //        return image;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        //public FileResult Image()
        //{
        //    byte[] image = GetBytesFromDB();
        //    return new FileContentResult(image, "image/jpeg");
        //}

    }
}