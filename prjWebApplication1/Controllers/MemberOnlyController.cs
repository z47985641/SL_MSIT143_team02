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
            return View();
        }
        [HttpPost]
        public IActionResult MemnerRoomNew(CRoomNew NewRoom)
        {
            MingSuContext db = new MingSuContext();
            Room room = new Room();
            room.RoomName = NewRoom.RoomName;
            room.RoomPrice = NewRoom.RoomPrice;
            room.RoomIntrodution = NewRoom.RoomIntrodution;
            room.MemberId = (int)HttpContext.Session.GetInt32("MemberID");
            room.RoomstatusId = 4;
            room.Address = NewRoom.Address;
            room.CreateDate = DateTime.Now;
            room.Qty = NewRoom.Qty;

            Image img = new Image();
            var ms = new MemoryStream();
            NewRoom.img.CopyTo(ms);
            img.Image1 = ms.ToArray();

            
            db.Rooms.Add(room);
            db.Images.Add(img);
            db.SaveChanges();
            ImageReference imgRef = new ImageReference();
            imgRef.RoomId = db.Rooms.OrderByDescending(i => i.RoomId).FirstOrDefault().RoomId + 1;
            imgRef.ImageId = db.Images.OrderByDescending(i => i.ImageId).FirstOrDefault().ImageId ;


            db.ImageReferences.Add(imgRef);
            db.SaveChanges();

            return RedirectToAction("TestListView","Room");
        }
        //public IActionResult PhotoCreateEdit(int? ItemId)
        //{
        //    MingSuContext db = new MingSuContext();
        //    if (photoData.MemberImage != null)
        //    {
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            byte[] b_photo = photoData.MemberImage;
        //            ms.Write(b_photo);
        //            return File(ms.ToArray(), "image/jpeg");
        //        }
        //    }
        //    return new EmptyResult();
        //}
    }
}
