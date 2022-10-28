using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Helpers;
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
        public IActionResult Index()
        {
            return View();
        }

        // GET: Shopping
        public ActionResult List(int? RoomId)
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
            MingSuContext db = new MingSuContext();
            CroomView crv = new CroomView();
            crv.房源 = (from c in db.Rooms
                      where c.RoomId == (int)RoomId
                      select new 房源
                      {
                          RoomName = c.RoomName,
                          RoomPrice = c.RoomPrice,
                          RoomIntrodution = c.RoomIntrodution,
                          MemberId = c.MemberId,
                          Address = c.Address,
                          CreateDate = c.CreateDate,
                          //FimagePath = c.FimagePath,
                          Qty = c.Qty,
                          Fimage = c.Fimage
                      }).ToList();
            return View(crv);
        }
        [HttpPost]
        public IActionResult AddToSession(CShoppingCartItem s)
        {
            string jsonBurchased = JsonSerializer.Serialize(s);
            HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS, jsonBurchased);
            return NoContent();
        }
        public IActionResult ShoppingCart()
        {
            MingSuContext db = new MingSuContext();

            var shopping = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS);
            var c = JsonSerializer.Deserialize<CCartCartItem>(shopping);
            CTranCartCartItem tsc = new CTranCartCartItem()
            {
                RoomId = c.RoomId,
                RoomName = db.Rooms.Where(a => a.RoomId == c.RoomId).FirstOrDefault().RoomName,
                count = c.count,
                price = db.Rooms.Where(a => a.RoomId == c.RoomId).FirstOrDefault().RoomPrice,
            };
            return View(tsc);
        }


        //public IActionResult CartView()
        //{
        //    if (HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS))
        //    {
        //        string jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS);
        //        List<CShoppingCartItem> cart = JsonSerializer.Deserialize<List<CShoppingCartItem>>(jsonCart);
        //        return View(cart);
        //    }
        //    else
        //        return RedirectToAction("List");
        //}
        //public IActionResult AddtoCart(int id)
        //{
        //    MingSuContext db = new MingSuContext();

        //    var product = db.Rooms.Single(x => x.RoomId.Equals(id));
        //    CShoppingCartItem item = new CShoppingCartItem()
        //    {
        //        RoomId= product.RoomId,
        //        Room = product,
        //        RoomName= product.RoomName,
        //        count = (int)product.Qty,
        //        price = product.RoomPrice

        //    }; if (SessionHelper.GetObjectFromJson<List<CShoppingCartItem>>(HttpContext.Session, "cart") == null)
        //    {
        //        //如果沒有已存在購物車: 建立新的購物車
        //        List<CShoppingCartItem> cart = new List<CShoppingCartItem>();
        //        cart.Add(item);
        //        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        //    }
        //    else
        //    {
        //        //如果已存在購物車: 檢查有無相同的商品，有的話只調整數量
        //        List<CShoppingCartItem> cart = SessionHelper.GetObjectFromJson<List<CShoppingCartItem>>(HttpContext.Session, "cart");

        //        int index = cart.FindIndex(m => m.Room.RoomId.Equals(id)); // FindIndex查詢位置

        //        if (HttpContext.Session.GetInt32("MemberID") == null)
        //        {
        //            return RedirectToAction("Login", "MemberCreate");
        //        }

        //        else
        //        {
        //            cart.Add(item);
        //        }
        //        SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
        //    }

        //    return NoContent(); // HttpStatus 204: 請求成功但不更新畫面
        //}
        //-----------------------------------------------------------------------------------------------------------
        //public IActionResult AddToCart(int/*?*/ id)
        //{

        //    MingSuContext db = new MingSuContext();
        //    Room prod = db.Rooms.FirstOrDefault(t => t.RoomId == id);
        //    if (prod == null)
        //        return RedirectToAction("List");
        //    return View(prod);

        //}
        //[HttpPost]
        //public IActionResult AddToCart(CAddToCartViewModel vModel)
        //{
        //    MingSuContext db = new MingSuContext();
        //    Room prod = db.Rooms.FirstOrDefault(t => t.RoomId == vModel.RoomId);
        //    if (prod == null)
        //        return RedirectToAction("List");
        //    string jsonCart = "";
        //    List<CShoppingCartItem> list = null;
        //    if (!HttpContext.Session.Keys.Contains(CDictionary.SK_PURCHASED_PRODUCTS))
        //        list = new List<CShoppingCartItem>();
        //    else
        //    {
        //        jsonCart = HttpContext.Session.GetString(CDictionary.SK_PURCHASED_PRODUCTS);
        //        list = JsonSerializer.Deserialize<List<CShoppingCartItem>>(jsonCart);
        //    }
        //    CShoppingCartItem item = new CShoppingCartItem()
        //    {
        //        count = vModel.Qty,
        //        price = (decimal)prod.RoomPrice,
        //        RoomId = vModel.RoomId,
        //        Room = prod,
        //        RoomName = prod.RoomName
        //    };
        //    list.Add(item);
        //    jsonCart = JsonSerializer.Serialize(list);
        //    HttpContext.Session.SetString(CDictionary.SK_PURCHASED_PRODUCTS, jsonCart);
        //    return RedirectToAction("List");
        //}
        //public  MingSuContext _context;
        //public  UserManager<IdentityUser> _userManager;

        //public AddHotelController(MingSuContext context, UserManager<IdentityUser> userManager)
        //{
        //    _context = context;
        //    _userManager = userManager;
        //}

        //結帳
        public IActionResult Checkout(CAddToCartViewModel c)
        {
            MingSuContext db = new MingSuContext();
            if (HttpContext.Session.GetInt32("MemberID") == null)
            {
                return RedirectToAction("Login", "MemberCreate");
            }
            else
            { 
                var Name = HttpContext.Session.GetString(CDictionary.SK_LOGINED_USER);
                var user = JsonSerializer.Deserialize<Member>(Name);

                CRoomMemberViewModel crm = new CRoomMemberViewModel();
                crm.房源及會員 = (from a in db.Rooms
                             where a.RoomId == c.RoomId
                             select new 房源及會員
                             {
                                 RoomId = a.RoomId,
                                 RoomName = a.RoomName,
                                 count = (int)a.Qty,
                                 price = a.RoomPrice,
                                 MemberId = user.MemberId,
                                 MemberName = user.MemberName,
                                 MemberEmail = user.MemberEmail,
                                 MemberPhone = user.MemberPhone,
                             }).ToList();
                return View(crm);
            }



        }


    }















}
