using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult List(CKeywordViewModel model)
        {
            MingSuContext hotel = new MingSuContext();
            IEnumerable<Order> datas = null;
            if (string.IsNullOrEmpty(model.txtKeyword))
                datas = from p in hotel.Orders
                        select p;
            else
                datas = hotel.Orders.Where(p => (p.OrderId).ToString().Contains(model.txtKeyword) ||
                p.OrderRemark.Contains(model.txtKeyword) ||
                p.MemberId.ToString().Contains(model.txtKeyword));
            return View(datas);
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                MingSuContext hotel = new MingSuContext();
                Order o = hotel.Orders.FirstOrDefault(h=>h.OrderId==id);
                if (o != null)
                {
                    hotel.Orders.Remove(o);
                    hotel.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                MingSuContext hotel = new MingSuContext();
                Order o = hotel.Orders.FirstOrDefault(h => h.OrderId == id);
                if (o != null)
                    return View(o);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Edit(Order inCust)
        {
            MingSuContext hotel = new MingSuContext();
            Order o = hotel.Orders.FirstOrDefault(h => h.OrderId == inCust.OrderId);
            if (o != null)
            {
                o.OrderId = inCust.OrderId;
                o.OrderRemark = inCust.OrderRemark;
                o.MemberId = inCust.MemberId;
                hotel.SaveChanges();
            }
            return RedirectToAction("List");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Order p)
        {
            MingSuContext hotel = new MingSuContext();
            hotel.Orders.Add(p);
            hotel.SaveChanges();
            return RedirectToAction("List");
        }


    }
}
