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
        //private readonly UserManager<IdentityUser> _userManager;


        //public async Task<IActionResult> Index()
        //{
        //    MingSuContext db = new MingSuContext();

        //    List<OrderViewModel> orderVM = new List<OrderViewModel>();

        //    var userId = _userManager.GetUserId(User);
        //    var orders = await db.Orders.
        //        OrderByDescending(k => k.OrderId).            //用OrderId排序
        //        Where(m => m.MemberId == Convert.ToInt32(userId)).ToListAsync();   //取得屬於當前登入者的訂單

        //    foreach (var item in db.Orders)
        //    {
        //        item.OrderItem = await db.OrderItem.
        //            Where(p => p.OrderId == item.OrderId).ToListAsync(); //取得訂單內的商品項目

        //        var ovm = new OrderViewModel()
        //        {
        //            Order = item,
        //            CartItems = GetOrderItems(item.OrderId)
        //        };

        //        orderVM.Add(ovm);
        //    }

        //    return View(orderVM);
        //}

        //訂單資訊
        //public async Task<IActionResult> Details(int? Id)
        //{
        //    MingSuContext db = new MingSuContext();

        //    if (Id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await db.Orders.FirstOrDefaultAsync(m => m.OrderId == Id);
        //    if (order.MemberId != Convert.ToInt32(_userManager.GetUserId(User)))
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        order.OrderItem = await db.OrderItem.Where(p => p.OrderId == Id).ToListAsync();
        //        ViewBag.orderItems = GetOrderItems(order.OrderId);
        //    }

        //    return View(order);
        //}



        // 結帳
        //public IActionResult Checkout(CAddToCartViewModel c)
        //{
        //    //確認 Session 內存在購物車
        //    if (SessionHelper.GetObjectFromJson<List<房源及會員>>(HttpContext.Session, "cart") == null)
        //    {
        //        return RedirectToAction("Index", "Cart");
        //    }
        //    MingSuContext db = new MingSuContext();
        //    if (HttpContext.Session.GetInt32("MemberID") == null)
        //    {
        //        return RedirectToAction("Login", "MemberCreate");
        //    }
        //    var Name = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
        //    var v = JsonSerializer.Deserialize<Member>(Name);
        //    //建立新的訂單
        //    Order myOrder = new Order()
        //    {
        //        MemberId=v.MemberId,
        //        OrderstatusId=1,
        //        RoomId=c.RoomId,
        //    };
        //    db.Orders.Add(myOrder);
        //    db.SaveChanges();
        //    //ViewBag.CartItems = SessionHelper.GetObjectFromJson<List<房源及會員>>(HttpContext.Session, "cart");

        //    return View(myOrder);

        //}

        //// 新增訂單到資料庫
        //[HttpPost]
        //public async Task<IActionResult> CreateOrder(Order order)
        //{
        //    //新增訂單到資料庫
        //    if (ModelState.IsValid)
        //    {
        //        MingSuContext db = new MingSuContext();
        //        order.OrderItem = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");

        //        db.Add(order);
        //        await db.SaveChangesAsync();
        //        SessionHelper.Remove(HttpContext.Session, "cart");

        //        return RedirectToAction("ReviewOrder", new { Id = order.OrderId });
        //    }
        //    return View();
        //}
        //// 取得當前訂單
        //public async Task<IActionResult> ReviewOrder(int? Id)
        //{
        //    MingSuContext db = new MingSuContext();

        //    if (Id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await db.Orders.FirstOrDefaultAsync(m => m.OrderId == Id);
        //    if (order.MemberId != Convert.ToInt32(_userManager.GetUserId(User)))
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        order.OrderItem = await db.OrderItem.Where(p => p.OrderId == Id).ToListAsync();
        //        ViewBag.orderItems = GetOrderItems(order.OrderId);
        //    }

        //    return View(order);
        //}
        ////    // 付款
        //public async Task<IActionResult> Payment(int? Id, bool isSuccess)
        //{
        //    MingSuContext db = new MingSuContext();

        //    if (Id == null)
        //    {
        //        return NotFound();
        //    }

        //    var order = await db.Orders.FirstOrDefaultAsync(p => p.OrderId == Id);
        //    if (order == null)
        //    {
        //        return NotFound();
        //    }
        //    else
        //    {
        //        if (isSuccess)
        //        {
        //            order.OrderstatusId = 1;
        //            db.Update(order);
        //            await db.SaveChangesAsync();  //更新訂單狀態
        //        }
        //        return RedirectToAction("ReviewOrder", new { Id = order.OrderId });
        //    }
        //}


        ////    // 取得商品詳細資料
        //private List<房源及會員> GetOrderItems(int orderId)
        //{
        //    MingSuContext db = new MingSuContext();

        //    var OrderItems = db.房源及會員.where(p => p.OrderId == orderId).ToList();

        //    List<房源及會員> orderItems = new List<房源及會員>();
        //    foreach (var orderitem in OrderItems)
        //    {
        //        房源及會員 item = new List<房源及會員>(orderitem);
        //        item.Room = db.Rooms.Single(x => x.RoomId == orderitem.RoomId);
        //        orderItems.Add(item);
        //    }

        //    return orderItems;
        //}


        //=========================================================================================


        public IActionResult PayData()
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
                            MemberName = v.MemberName,
                            MemberEmail = v.MemberEmail,
                            MemberPhone = v.MemberPhone,
                            RoomId = p.RoomId,
                            RoomName = p.RoomName,
                             price= p.RoomPrice,
                            count = Convert.ToInt32(p.Qty),
                        }).ToList();
            return View(crv);
        }

        public IActionResult GetPayData(string jsonString)
        {
            
            //var a = JsonSerializer.Deserialize<CAddToCartViewModel>(jsonString);
            HttpContext.Session.SetString(CDictionary.SK_CHECK_OUT, jsonString);
            return Content("1");
        }




        public IActionResult PayCheckout(CAddToCartViewModel p)
        {
            MingSuContext db = new MingSuContext();

            var Name = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            var v = JsonSerializer.Deserialize<Member>(Name);

            CRoomMemberViewModel crv = new CRoomMemberViewModel();

            crv.房源及會員 = (from c in db.Rooms
                         where c.RoomId == p.RoomId
                         select new 房源及會員
                         {
                             MemberName = v.MemberName,
                             MemberEmail = v.MemberEmail,
                             MemberPhone = v.MemberPhone,
                             RoomId = c.RoomId,
                             RoomName = c.RoomName,
                             price = c.RoomPrice,
                             count = p.Qty,
                         }).ToList();
            return View(crv);
        }
        public IActionResult PayEnd(CAddToCartViewModel p)
        {
            MingSuContext db = new MingSuContext();

            var Name = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
            var v = JsonSerializer.Deserialize<Member>(Name);
            Order od = new Order()
            {
                MemberId = v.MemberId,
                RoomId = p.RoomId,
                OrderstatusId = 4,   //1已成立  4加入購物車
            };
            db.Orders.Add(od);
            db.SaveChanges();

            OrderDetail odd = new OrderDetail()
            {
                OrderId = db.Orders.OrderBy(e => e.OrderId).LastOrDefault().OrderId,
                RoomId = p.RoomId,
                OrderPrice = p.RoomPrice,
            };
            db.OrderDetails.Add(odd);
            db.SaveChanges();
            System.Threading.Thread.Sleep(3000);
            return RedirectToAction("Index", "Home");
        }
















    }
}
