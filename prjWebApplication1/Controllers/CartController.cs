﻿using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Helpers;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class CartController : Controller
    {

        public IActionResult Index()
        {
            List<CartItem> CartItems = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

            if (CartItems != null)
            {
                ViewBag.Total = CartItems.Sum(m => m.room.RoomPrice*m.room.Qty); // 計算商品總額
            }
            else
            {
                ViewBag.Total = 0;
            }

            return View(CartItems);
        }


        public IActionResult AddtoCart(int id)
        {
            MingSuContext db = new MingSuContext();

            //var product = db.Rooms.Single(x => x.RoomId.Equals(id));
            //房源及會員 item = new 房源及會員()
            //{
            //    RoomId = product.RoomId,
            //    RoomName = product.RoomName,
            //    count = 1,
            //    price = product.RoomPrice,
            //    Room= product

            //};

            //取得商品資料
            CartItem item = new CartItem
            {
                room = db.Rooms.Single(x => x.RoomId.Equals(id)),
                Amount = 1,
                SubTotal = Convert.ToInt32(db.Rooms.Single(m => m.RoomId == id).RoomPrice)
            };

            if (SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart") == null)
            {
                //如果沒有已存在購物車: 建立新的購物車
                List<CartItem> cart = new List<CartItem>();
                cart.Add(item);
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                //如果已存在購物車: 檢查有無相同的商品，有的話只調整數量
                List<CartItem> cart = SessionHelper.GetObjectFromJson<List<CartItem>>(HttpContext.Session, "cart");

                int index = cart.FindIndex(m => m.RoomId.Equals(id)); // FindIndex查詢位置

                if (index != -1)
                {
                    cart[index].Amount += item.Amount;
                    cart[index].SubTotal += item.SubTotal;
                }
                else
                {
                    cart.Add(item);
                }
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return NoContent(); // HttpStatus 204: 請求成功但不更新畫面
        }

        public IActionResult RemoveItem(int id)
        {
            List<房源及會員> cart = SessionHelper.GetObjectFromJson<List<房源及會員>>(HttpContext.Session, "cart");
            int index = cart.FindIndex(m => m.Room.RoomId.Equals(id)); //FindIndex查詢位置
            cart.RemoveAt(index);

            if (cart.Count < 1)
            {
                SessionHelper.Remove(HttpContext.Session, "cart");
            }
            else
            {
                SessionHelper.SetObjectAsJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index");
        }

        //private string ViewImage(byte[] arrayImage)
        //{
        //    string base64String = Convert.ToBase64String(arrayImage, 0, arrayImage.Length);
        //    return "data:image/png;base64," + base64String;
        //}
    }

}
