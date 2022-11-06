using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModel;
using PJ_MSIT143_team02.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
            IEnumerable<Room> datas = null;
            if (string.IsNullOrEmpty(model.txtKeyword))
                datas = from p in db.Rooms
                        select p;
            else
                datas = db.Rooms.Where(p => p.RoomName.Contains(model.txtKeyword)
                || p.RoomPrice.ToString().Contains(model.txtKeyword)
                || p.RoomIntrodution.Contains(model.txtKeyword)
                || p.MemberId.ToString().Contains(model.txtKeyword)
                || p.RoomstatusId.ToString().Contains(model.txtKeyword)
                || p.Address.Contains(model.txtKeyword)
                || p.CreateDate.ToString().Contains(model.txtKeyword)
                || p.Qty.ToString().Contains(model.txtKeyword));
            return View(datas);
        }

        public IActionResult ListView(CKeywordViewModel model)
        {
            MingSuContext db = new MingSuContext();
            List<CAllViewModel> cAlls = new List<CAllViewModel>();
            var q = from r in db.Rooms
                    select r;
            IEnumerable<Room> datas = null;
            if (string.IsNullOrEmpty(model.txtKeyword))

                datas = from r in db.Rooms
                        join i in db.ImageReferences on r.RoomId equals i.RoomId
                        join k in db.Images on i.ImageId equals k.ImageId
                        join m in db.Members on r.MemberId equals m.MemberId
                        join l in db.RoomStatuses on r.RoomstatusId equals l.RoomstatusId
                        select r;
            else
                datas = db.Rooms.Where(p => p.RoomName.Contains(model.txtKeyword)
                || p.RoomPrice.ToString().Contains(model.txtKeyword)
                || p.RoomIntrodution.Contains(model.txtKeyword)
                || p.MemberId.ToString().Contains(model.txtKeyword)
                || p.RoomstatusId.ToString().Contains(model.txtKeyword)
                || p.Address.Contains(model.txtKeyword)
                || p.CreateDate.ToString().Contains(model.txtKeyword));
            return View(cAlls);
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

            //IEnumerable<Equipment> eq = null;
            //if (string.IsNullOrEmpty(ck.txtKeyword))
            //    eq = (from r in db.Equipment
            //         join i in db.EquipmentReferences on r.EquipmentId equals i.EquipmentId
            //         join k in db.Rooms on i.RoomId equals k.RoomId
            //         select r).ToList();
            IEnumerable<EquipmentReference> eqr = null;
            if (string.IsNullOrEmpty(ck.txtKeyword))
                eqr = (from r in db.EquipmentReferences
                       join i in db.Equipment on r.EquipmentId equals i.EquipmentId
                       join k in db.Rooms on r.RoomId equals k.RoomId
                       select r).ToList();






                IEnumerable<Room> datas = null;
            if (string.IsNullOrEmpty(ck.txtKeyword)) {
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
                    datas =(from r in db.Rooms
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
            else {
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
            int EquipmentID=0;
            EquipmentID= int.Parse(Equipment) ;
            //eq = (from r in db.Equipment
            //      join i in db.EquipmentReferences on r.EquipmentId equals i.EquipmentId
            //      join k in db.Rooms on i.RoomId equals k.RoomId
            //      select r).ToList();
            int RoomId=0;
            IQueryable<EquipmentReference> eq = null;
            if (RoomId == 0)
            {
                eq = from s in db.EquipmentReferences
                         where s.EquipmentId == EquipmentID
                         select s;
                ViewBag.name = (eq.ToList().Count() != 0) ? "設備>>>>>" + eq.FirstOrDefault().Room.RoomName : "查無此設備";
            }
            else if (EquipmentID == 0)
            {
                eq = from s in db.EquipmentReferences
                     where s.RoomId == RoomId
                           select s;
                ViewBag.name = (eq.ToList().Count() != 0) ? "設備>>>>>" + eq.FirstOrDefault().Room.RoomName : "查無此商品";
            }
            CKeywordViewModel ck = new CKeywordViewModel();
            ck.EquipmentReference = eq;
            return PartialView("forEquipment", ck);




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
        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                MingSuContext db = new MingSuContext();
                Room r = db.Rooms.FirstOrDefault(t => t.RoomId == id);
                if (r != null)
                {
                    db.Rooms.Remove(r);
                    db.SaveChanges();
                }

            }
            return RedirectToAction("List");
        }
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                MingSuContext db = new MingSuContext();
                Room r = db.Rooms.FirstOrDefault(t => t.RoomId == id);
                if (r != null)
                    return View(r);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public ActionResult Edit(CRoomViewModel InRoom)
        {

            MingSuContext db = new MingSuContext();
            Room r = db.Rooms.FirstOrDefault(t => t.RoomId == InRoom.RoomId);
            if (r != null)
            {
                if (InRoom.photo != null)
                {
                    string pName = Guid.NewGuid().ToString() + ".jpg";
                    r.FimagePath = pName;
                    string path = _enviro.WebRootPath + "/image/" + pName;
                    InRoom.photo.CopyTo(new FileStream(path, FileMode.Create));
                }


                r.RoomId = InRoom.RoomId;
                r.RoomName = InRoom.RoomName;
                r.RoomPrice = InRoom.RoomPrice;
                r.RoomIntrodution = InRoom.RoomIntrodution;
                r.MemberId = InRoom.MemberId;
                r.RoomstatusId = InRoom.RoomstatusId;
                r.Address = InRoom.Address;
                r.CreateDate = InRoom.CreateDate;
                r.Qty = InRoom.Qty;
                r.FimagePath = InRoom.FimagePath;
                db.SaveChanges();
            }
            return RedirectToAction("List", "path");
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
