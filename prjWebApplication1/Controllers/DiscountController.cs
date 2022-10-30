using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.ViewModels;
using PJ_MSIT143_team02.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class DiscountController : Controller
    {
        public IActionResult DiscountMain()
        {
            var data = queryAll();
            return View(data);
        }

        public IActionResult Details(int? Id)
        {
            if (Id != null)
            {
                MingSuContext db = new MingSuContext();
                Discount d = db.Discounts.FirstOrDefault(d => d.RoomDiscountId == Id);
                if (d != null)
                    return View(d);
            }
            return RedirectPermanent("DiscountMain");
        }

        public IActionResult DiscountAdmin()
        {
            var data = queryAll();
            return View(data);
        }
        [HttpPost]
        public IActionResult DiscountAdmin(DisViewModel model)
        {
            IEnumerable<Discount> data;
            if (string.IsNullOrEmpty(model.txtKey))
                data = queryAll();
            else
                data = from d in (new MingSuContext()).Discounts
                       where (d.DiscountInfo.Contains(model.txtKey) ||
                       d.DiscountName.Contains(model.txtKey) ||
                       d.DiscountValue.ToString().Contains(model.txtKey))
                       select d;
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Discount d)
        {
            if (string.IsNullOrEmpty(d.DiscountInfo) || string.IsNullOrEmpty(d.DiscountName)
                || string.IsNullOrEmpty(d.DiscountValue.ToString()))
                return View();
            MingSuContext db = new MingSuContext();
            db.Discounts.Add(d);
            db.SaveChanges();
            return RedirectPermanent("DiscountAdmin");
        }

        public IActionResult Delete(int? Id)
        {
            MingSuContext db = new MingSuContext();
            Discount d = db.Discounts.FirstOrDefault(d => d.RoomDiscountId == Id);
            if (d != null)
            {
                db.Discounts.Remove(d);
                db.SaveChanges();
            }
            return RedirectToAction("DiscountAdmin");
        }

        public IActionResult Edit(int? Id)
        {
            if (Id != null)
            {
                MingSuContext db = new MingSuContext();
                Discount d = db.Discounts.FirstOrDefault(d => d.RoomDiscountId == Id);
                if (d != null)
                    return View(d);
            }
            return RedirectPermanent("DiscountAdmin");
        }
        [HttpPost]
        public IActionResult Edit(DisViewModel input)
        {
            MingSuContext db = new MingSuContext();
            Discount d = db.Discounts.FirstOrDefault(d => d.RoomDiscountId == input.Id);
            if (d != null)
            {
                d.DiscountInfo = input.DiscountInfo;
                d.DiscountName = input.DiscountName;
                db.SaveChanges();
            }
            return RedirectToAction("DiscountAdmin");
        }
        public IEnumerable<Discount> queryAll()
        {
            var data = from d in (new MingSuContext()).Discounts
                       select d;
            return data;
        }
    }
}
