using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
    }
}
