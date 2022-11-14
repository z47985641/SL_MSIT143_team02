using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PJ_MSIT143_team02.Helpers;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class CheckOutController : Controller
    {
        public IActionResult PayData()     //訂單資料
        {
            MingSuContext db = new MingSuContext();

            var checkout = HttpContext.Session.GetString(CDictionary.SK_CHECK_OUT);
            var p = JsonSerializer.Deserialize<CAddToCart>(checkout);

            var Name = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            var v = JsonSerializer.Deserialize<Member>(Name);

            CRoomMemberViewModel crv = new CRoomMemberViewModel();
            //註記用//
            crv.房源及會員 = (from c in db.Rooms
                         where c.RoomId == p.RoomId
                         select new 房源及會員
                         {
                             MemberId = v.MemberId,
                             MemberAccount = v.MemberAccount,
                             MemberName = v.MemberName,
                             MemberEmail = v.MemberEmail,
                             MemberPhone = v.MemberPhone,
                             RoomId = (p.RoomId == 0 ? p.RoomId : p.ActivityId),
                             RoomName = (p.ActivityName == null? p.RoomName: p.ActivityName),
                             price =(p.ActivityPrice==0?p.RoomPrice:p.ActivityPrice),
                             count =Convert.ToInt32(p.count==null?p.Qty:p.count),
                             DisPrice = p.DisPrice,
                         }).ToList();
            return View(crv);


        }
        public IActionResult GetPayData(string jsonString)
        {
            HttpContext.Session.SetString(CDictionary.SK_CHECK_OUT, jsonString);
            return Content("1");
        }

        public IActionResult CreateOrder()   //建立訂單入資料庫
        {
            MingSuContext db = new MingSuContext();

            var checkout = HttpContext.Session.GetString(CDictionary.SK_CHECK_OUT);
            var p = JsonSerializer.Deserialize<CAddToCart>(checkout);

            var Name = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            var v = JsonSerializer.Deserialize<Member>(Name);

            CRoomMemberViewModel crv = new CRoomMemberViewModel();
            crv.房源及會員 = (from c in db.Rooms
                         where c.RoomId == p.RoomId
                         select new 房源及會員
                         {
                             MemberId = v.MemberId,
                             MemberAccount = v.MemberAccount,
                             MemberName = v.MemberName,
                             MemberEmail = v.MemberEmail,
                             MemberPhone = v.MemberPhone,
                             RoomId = (p.RoomId == 0 ? p.RoomId : p.ActivityId),
                             RoomName = (p.ActivityName == null ? p.RoomName : p.ActivityName),
                             price = (p.ActivityPrice == 0 ? p.RoomPrice : p.ActivityPrice),
                             count = Convert.ToInt32(p.count == null ? p.Qty : p.count),
                             DisPrice = p.DisPrice,
                         }).ToList();

            Order od = new Order()
            {
                MemberId = v.MemberId,
                RoomId = p.RoomId,
                OrderstatusId = 4,   //1已成立  4加入購物車
            };
            db.Orders.Add(od);
            db.SaveChanges();
            return View(crv);
        }

    }
}
