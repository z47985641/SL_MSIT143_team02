using Microsoft.AspNetCore.Http;
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
        public IActionResult List()
        {
            MingSuContext db = new MingSuContext();
            COrderdetailViewModel vb = new COrderdetailViewModel();
            IEnumerable<COrderdetailViewModel> order = (from o in db.Orders
                                                        join m in db.OrderDetails on o.OrderId equals m.OrderId
                                                        join s in db.OrderStatuses on o.OrderstatusId equals s.OrderstatusId
                                                        join r in db.Rooms on o.RoomId equals r.RoomId
                                                        select new COrderdetailViewModel()
                                                        {
                                                            OrderId = o.OrderId,
                                                            OrderRemark = o.OrderRemark,
                                                            MemberId = o.MemberId,
                                                            OrderstatusId = o.OrderstatusId,
                                                            RoomId = o.RoomId,
                                                            ActivityId = o.ActivityId,
                                                            OrderPrice = m.OrderPrice,
                                                            OrderStartDate = m.OrderStartDate,
                                                            OrderEndDate = m.OrderEndDate,
                                                            Payment = m.Payment,
                                                            Qty = m.Qty,
                                                            OrderstatusContent = s.OrderstatusContent,
                                                            RoomName = r.RoomName,


                                                        }).ToList();

            return View(order);
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
                o.OrderstatusId = inCust.OrderstatusId;
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
        public IActionResult AddtoCart()
        {
            return View();
        }
        public IActionResult AddtoLike(int? id)
        {
            MingSuContext db = new MingSuContext();
            Room datas = db.Rooms.FirstOrDefault(Room_datas => Room_datas.RoomId == id);




            return View();
        }
    }
}
