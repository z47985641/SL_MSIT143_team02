using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PJ_MSIT143_team02.Helpers;
using PJ_MSIT143_team02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class CheckOutController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public IActionResult Index()
        {
            return View();
        }
        // 結帳
        public IActionResult Checkout()
        {
            //確認 Session 內存在購物車
            if (SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart") == null)
            {
                return RedirectToAction("Index", "Cart");
            }

            //建立新的訂單
            var myOrder = new Order()
            {
                 //= Convert.ToInt32(_userManager.GetUserId(User)),
                MemberId = Convert.ToInt32(_userManager.GetUserName(User)),
                OrderItem = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart")
            };
            //myOrder.Total = myOrder.OrderItem.Sum(m => m.SubTotal);
            ViewBag.CartItems = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

            return View(myOrder);
        }

        // 新增訂單到資料庫
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            //新增訂單到資料庫
            if (ModelState.IsValid)
            {
                MingSuContext db = new MingSuContext();
                order.OrderItem = SessionHelper.GetObjectFromJson<List<OrderItem>>(HttpContext.Session, "cart");

                db.Add(order);
                await db.SaveChangesAsync();
                SessionHelper.Remove(HttpContext.Session, "cart");

                return RedirectToAction("ReviewOrder", new { Id = order.OrderId});
            }
            return View();
        }
        // 取得當前訂單
        public async Task<IActionResult> ReviewOrder(int? Id)
        {
            MingSuContext db = new MingSuContext();

            if (Id == null)
            {
                return NotFound();
            }

            var order = await db.Orders.FirstOrDefaultAsync(m => m.OrderId == Id);
            if (order.MemberId!= Convert.ToInt32(_userManager.GetUserId(User)))
            {
                return NotFound();
            }
            else
            {
                order.OrderItem = await db.OrderItem.Where(p => p.OrderId == Id).ToListAsync();
                ViewBag.orderItems = GetOrderItems(order.OrderId);
            }

            return View(order);
        }
        // 付款
        public async Task<IActionResult> Payment(int? Id, bool isSuccess)
        {
            MingSuContext db = new MingSuContext();

            if (Id == null)
            {
                return NotFound();
            }

            var order = await db.Orders.FirstOrDefaultAsync(p => p.OrderId == Id);
            if (order == null)
            {
                return NotFound();
            }
            else
            {
                if (isSuccess)
                {
                    order.OrderstatusId = 1;
                    db.Update(order);
                    await db.SaveChangesAsync();  //更新訂單狀態
                }
                return RedirectToAction("ReviewOrder", new { Id = order.OrderId });
            }
        }


        // 取得商品詳細資料
        private List<CartItem> GetOrderItems(int orderId)
        {
            MingSuContext db = new MingSuContext();

            var OrderItems = db.OrderItem.Where(p => p.OrderId == orderId).ToList();

            List<CartItem> orderItems = new List<CartItem>();
            foreach (var orderitem in OrderItems)
            {
                CartItem item = new CartItem(orderitem);
                item.room = db.Rooms.Single(x => x.RoomId == orderitem.RoomId);
                orderItems.Add(item);
            }

            return orderItems;
        }






















    }
}
