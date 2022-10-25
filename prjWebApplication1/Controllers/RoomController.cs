using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModels;
using PJ_MSIT143_team02.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace PJ_MSIT143_team02.Controllers
{
    public class RoomController : Controller
    {
        //private MingSuContext MS;
        //public RoomController(MingSuContext q)
        //{
        //    MS = q;
        //}
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
       
        public IActionResult TestListView(CKeywordViewModel model)
        {
            MingSuContext db = new MingSuContext();
            IEnumerable<Room> datas = null;
            if (string.IsNullOrEmpty(model.txtKeyword))

                datas = from r in db.Rooms
                            //join i in db.ImageReferences on r.RoomId equals i.RoomId
                            //join k in db.Images on i.ImageId equals k.ImageId
                            //join m in db.Members on r.MemberId equals m.MemberId
                            //join l in db.RoomStatuses on r.RoomstatusId equals l.RoomstatusId
                        select r;
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
                //if (InRoom.photo != null)
                //{
                //    string pName = Guid.NewGuid().ToString() + ".jpg";
                //    r.FImagePath = pName;
                //    string path = _enviro.WebRootPath + "/images/" + pName;
                //    InRoom.photo.CopyTo(new FileStream(path, FileMode.Create));
                //}
                r.RoomId = InRoom.RoomId;
                r.RoomName = InRoom.RoomName;
                r.RoomPrice = InRoom.RoomPrice;
                r.RoomIntrodution = InRoom.RoomIntrodution;
                r.MemberId = InRoom.MemberId;
                r.RoomstatusId = InRoom.RoomstatusId;
                r.Address = InRoom.Address;
                r.CreateDate = InRoom.CreateDate;
                r.Qty= InRoom.Qty;
                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult Details(int? Id)
        {
            if (Id != null)
            {
                MingSuContext db = new MingSuContext();
                Room d = db.Rooms.FirstOrDefault(d => d.RoomId == Id);
                if (d != null)
                    return View(d);
            }
            return RedirectPermanent("TestListView");
        }
        //[HttpPost]
        //public IActionResult AddToSession(CAddToCartViewModel s)
        //{
        //    string jsonBurchased = JsonSerializer.Serialize(s);
        //    HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS, jsonBurchased);
        //    return NoContent();
        //}
        //public IActionResult ShoppingCart()
        //{
        //    var shopping = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS);
        //    var c = JsonSerializer.Deserialize<CAddToCartViewModel>(shopping);
        //    CGoShoppingCartView go = new CGoShoppingCartView()
        //    {
        //        RoomId = c.RoomId,
        //        RoomName = MS.Rooms.Where(a=>a.RoomId==c.RoomId).FirstOrDefault().RoomName,
        //        Qty = c.Qty,
        //        RoomPrice = MS.Rooms.Where(a => a.RoomId == c.RoomId).FirstOrDefault().RoomPrice,
        //    };
        //    return View(go);
        //}












    }
}
