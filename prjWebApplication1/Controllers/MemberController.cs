using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PJ_MSIT143_team02.Models;
using Microsoft.AspNetCore.Http;
using PJ_MSIT143_team02.ViewModels;
using System.IO;

namespace PJ_MSIT143_team02.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult MemberList(CMenberSearch KW)
        {
            MingSuContext db = new MingSuContext();
            IEnumerable<Member> datas = from I in db.Members
                                            //join A in db.Cities
                                            //on I.CityName equals A.CityId
                                        select I;
            if (KW.KW_ID > 0)
                datas = datas.Where(p => p.MemberId.ToString().Contains(KW.KW_ID.ToString()));
            if (!string.IsNullOrEmpty(KW.KW_MemberAccount))
                datas = datas.Where(p => p.MemberAccount.Contains(KW.KW_MemberAccount));
            if (!string.IsNullOrEmpty(KW.KW_MemberName))
                datas = datas.Where(p => p.MemberName.Contains(KW.KW_MemberName));
            if (!string.IsNullOrEmpty(KW.KW_MemberPhone))
                datas = datas.Where(p => p.MemberPhone.Contains(KW.KW_MemberPhone));
            if (!string.IsNullOrEmpty(KW.KW_MemberEmail))
                datas = datas.Where(p => p.MemberEmail.Contains(KW.KW_MemberEmail));
            if (!string.IsNullOrEmpty(KW.KW_Authority))
                datas = datas.Where(p => p.Authority.Contains(KW.KW_Authority));

            return View(datas);
        }
        public IActionResult MemberEdit(int? MemberId)
        {
            MingSuContext db = new MingSuContext();
            Member datas = db.Members.FirstOrDefault(Member => Member.MemberId == MemberId);
            return View(datas);
        }
        [HttpPost]
        public IActionResult MemberEdit(Member datasedit, IFormFile photoEdit)
        {
            MingSuContext db = new MingSuContext();

            Member datas = db.Members.FirstOrDefault(Member => Member.MemberId == datasedit.MemberId);
            datas.MemberEmail = datasedit.MemberEmail;
            datas.MemberAccount = datasedit.MemberAccount;
            datas.MemberName = datasedit.MemberName;
            datas.MemberPhone = datasedit.MemberPhone;
            datas.Authority = datasedit.Authority;
            datas.CityName = datasedit.CityName;
            datas.BirthDate = datasedit.BirthDate;
            if (photoEdit != null)
            {
                var ms = new MemoryStream();
                photoEdit.CopyTo(ms);
                datas.MemberImage = ms.ToArray();
            }

            db.SaveChanges();

            return RedirectToAction("MemberList");
        }
        public IActionResult MemberDelete(int? MemberId)
        {
            MingSuContext db = new MingSuContext();
            Member c = db.Members.FirstOrDefault(t => t.MemberId == MemberId);
            if (c != null)
            {
                db.Members.Remove(c);
                db.SaveChanges();
            }
            return RedirectToAction("MemberList");
        }

        public IActionResult MemberCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MemberCreate(Member member)
        {
            MingSuContext db = new MingSuContext();
            db.Members.Add(member);
            db.SaveChanges();
            return RedirectToAction("MemberList");
        }

        public IActionResult MemberPersonalEdit()
        {
            MingSuContext db = new MingSuContext();
            Member datas = db.Members.FirstOrDefault(Member => Member.MemberId == HttpContext.Session.GetInt32("MemberID"));
            return View(datas);
        }
        [HttpPost]
        public IActionResult MemberPersonalEdit(Member datasedit, IFormFile photoEdit)
        {
            MingSuContext db = new MingSuContext();
            Member datas = db.Members.FirstOrDefault(Member => Member.MemberId == datasedit.MemberId);
            datas.MemberEmail = datasedit.MemberEmail;
            datas.MemberAccount = datasedit.MemberAccount;
            datas.MemberName = datasedit.MemberName;
            datas.MemberPhone = datasedit.MemberPhone;
            datas.BirthDate = datasedit.BirthDate;
            datas.CityName = datasedit.CityName;
            if (photoEdit != null)
            {
                var ms = new MemoryStream();
                photoEdit.CopyTo(ms);
                datas.MemberImage = ms.ToArray();
            }
            db.SaveChanges();

            return RedirectToAction("MemberPersonalData");
        }

        public IActionResult MemberPersonalData()
        {
            MingSuContext db = new MingSuContext();
            IEnumerable<Member> datas = from I in db.Members
                                        where I.MemberId == HttpContext.Session.GetInt32("MemberID")
                                        select I;
            return View(datas);
        }

        public IActionResult MemberPasswordEdit()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MemberPasswordEdit(CMemberPassword datasedit)
        {
            MingSuContext db = new MingSuContext();
            Member datas = db.Members.FirstOrDefault(Member => Member.MemberId == HttpContext.Session.GetInt32("MemberID"));
            if (datasedit.MemberPassword != datas.MemberPassword)
                return View();

            datas.MemberPassword = datasedit.MemberNewPassword;
            db.SaveChanges();
            return RedirectToAction("MemberPersonalData");
        }
        public IActionResult LikeList()
        {
            MingSuContext db = new MingSuContext();
            var datas = from I in db.Orders
                        join R in db.Rooms on I.RoomId equals R.RoomId
                        where I.OrderstatusId == 5 && I.MemberId == HttpContext.Session.GetInt32("MemberID")
                        select new CLikelist
                        {
                            OrderId = I.OrderId,
                            RoomID = R.RoomId,
                            RoomName = R.RoomName,
                            RoomPrice = R.RoomPrice,
                            RoomIntrodution = R.RoomIntrodution,
                            Address = R.Address,
                            Qty = (int)R.Qty,
                        };
            return View(datas);
        }
        public IActionResult AddLikeList(int? ItemId)
        {
            MingSuContext db = new MingSuContext();
            Order Likeitem = new Order();
            Likeitem.MemberId = (int)HttpContext.Session.GetInt32("MemberID");
            Likeitem.OrderstatusId = 5;
            Likeitem.RoomId = (int)ItemId;
            db.Orders.Add(Likeitem);
            db.SaveChanges();

            return RedirectToAction("TestListView", "Room");
        }
        public IActionResult DeleteLikeList(int? ItemId)
        {
            MingSuContext db = new MingSuContext();
            Order deleteItem = db.Orders.FirstOrDefault(t => t.OrderId == ItemId);
            if (deleteItem != null)
            {
                db.Orders.Remove(deleteItem);
                db.SaveChanges();
            }
            return RedirectToAction("LikeList");
        }
        public IActionResult MemberPhoto(int? ItemId)
        {
            MingSuContext db = new MingSuContext();
            var photoData = db.Members.FirstOrDefault(t => t.MemberId == ItemId);
            if (photoData.MemberImage != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] b_photo = photoData.MemberImage;
                    ms.Write(b_photo);
                    return File(ms.ToArray(), "image/jpeg");
                }
            }
            return new EmptyResult();

        }
        public IActionResult MemberPhotoEdit(int? ItemId)
        {
            MingSuContext db = new MingSuContext();
            var photoData = db.Members.FirstOrDefault(t => t.MemberId == ItemId);
            if (photoData.MemberImage != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    byte[] b_photo = photoData.MemberImage;
                    ms.Write(b_photo);
                    return File(ms.ToArray(), "image/jpeg");
                }
            }

            return new EmptyResult();

        }
        public IActionResult LikeListPhoto(int? ItemId)
        {
            MingSuContext db = new MingSuContext();
            CLikelist CL = new CLikelist();
            byte[] b_photo = null;
            var photoID = from M in db.ImageReferences
                          join MI in db.Images
                          on M.ImageId equals MI.ImageId
                          where M.RoomId == ItemId
                          select new CLikelist
                          {
                              Image = MI.Image1
                          }
                           ;

            if (photoID != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    foreach (var I in photoID)
                        b_photo = I.Image;
                    ms.Write(b_photo);
                    return File(ms.ToArray(), "image/jpeg");
                }
            }
            return new EmptyResult();
        }
        //System.Drawing.Image.FromFile
    }
}
