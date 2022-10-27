using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModel;
using PJ_MSIT143_team02.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class AddHotelController : Controller
    {
        //private MingSuContext MS;
        //public AddHotelController(MingSuContext q)
        //{
        //    MS = q;
        //}

        public IActionResult Index()
        {
            return View();
        }

        // GET: Shopping
        public ActionResult List()
        {
            var datas = from t in (new MingSuContext()).Rooms
                        select t;
            List<CRoomViewModel> list = new List<CRoomViewModel>();
            foreach (Room t in datas)
            {
                CRoomViewModel vm = new CRoomViewModel();
                vm.room = t;
                list.Add(vm);
            }
            return View(list);
        }
        public IActionResult CartView()
        {
            if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS))
            {
                string jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS);
                List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(jsonCart);
                return View(cart);
            }
            else
                return RedirectToAction("List");

        }
        public IActionResult AddToCart(int? id)
        {

            MingSuContext db = new MingSuContext();
            Room prod = db.Rooms.FirstOrDefault(t => t.RoomId == id);
            if (prod == null)
                return RedirectToAction("List");
            return View(prod);
        }
        [HttpPost]
        public IActionResult AddToCart(CAddToCartViewModel vModel)
        {
            MingSuContext db = new MingSuContext();
            Room prod = db.Rooms.FirstOrDefault(t => t.RoomId == vModel.RoomId);
            if (prod == null)
                return RedirectToAction("List");
            string jsonCart = "";
            List<CShoppingCartItem> list = null;
            if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS))
                list = new List<CShoppingCartItem>();
            else
            {
                jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS);
                list = JsonSerializer.Deserialize<List<CShoppingCartItem>>(jsonCart);
            }
            CShoppingCartItem item = new CShoppingCartItem()
            {
                count = vModel.Qty,
                price = (decimal)prod.RoomPrice,
                RoomId = vModel.RoomId,
                Room = prod,
                RoomName= prod.RoomName
            };
            list.Add(item);
            jsonCart = JsonSerializer.Serialize(list);
            HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS, jsonCart);
            return RedirectToAction("List");
        }

        //結帳
        public IActionResult Checkout(CCheckOutDataViewModel c)
        {
            MingSuContext db = new MingSuContext();
            if (HttpContext.Session.GetInt32("MemberID") == null)
            {
                return RedirectToAction("Login", "MemberCreate");
            }
            else
            { //int MemberID = HttpContext.Session.GetInt32("MemberID") as Member
                var Name = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                var user = JsonSerializer.Deserialize<Member>(Name);
                var Name1 = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS);
                var user1= JsonSerializer.Deserialize<CShoppingCartItem>(Name1);

                CRoomMemberViewModel crm = new CRoomMemberViewModel();
                crm.房源及會員 = (from a in db.Rooms
                             where a.RoomId == user1.RoomId
                             select new 房源及會員
                             {
                                 RoomId = a.RoomId,
                                 RoomName = a.RoomName,
                                 count = a.Qty,
                                 price = a.RoomPrice,
                                 MemberId = user.MemberId,
                                 MemberName = user.MemberName,
                                 MemberEmail = user.MemberEmail,
                                 MemberPhone = user.MemberPhone,

                             }).ToList();
                return View(crm);
            };
        }








    }
}
