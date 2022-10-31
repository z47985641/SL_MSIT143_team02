using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.ViewModel;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class ActivityController : Controller
    {
        private IWebHostEnvironment _enviro;
        public ActivityController(IWebHostEnvironment p)
        {
            _enviro = p;
        }
        public IEnumerable<Activity> queryAll()
        {
            var data = from d in (new MingSuContext()).Activities
                       select d;
            return data;
        }
        public IActionResult ActivityDisplay()
        {
            var data = queryAll();
            return View(data);
        }
        public IActionResult Details(int? id)
        {
            if (id != null)
            {
                MingSuContext db = new MingSuContext();
                Activity act = db.Activities.FirstOrDefault(p => p.ActivityId == id);
                if (act != null)
                    return View(act);
            }
            return RedirectPermanent("ActivityDisplay");
        }
        public IActionResult List(CKeywordViewModel model)
        {            
            MingSuContext db = new MingSuContext();
            IEnumerable<Activity> datas = null;
            if (string.IsNullOrEmpty(model.txtKeyword))
            {
                datas = from p in db.Activities
                        select p;
            }
            else
                datas = db.Activities.Where(p => p.ActivityName.Contains(model.txtKeyword)
                || p.ActivityDate.ToString().Contains(model.txtKeyword)
                || p.ActivityCapacity.ToString().Contains(model.txtKeyword)
                || p.ActivityStatus.Contains(model.txtKeyword)
                || p.ActivityInfo.Contains(model.txtKeyword)
                || p.ActivityLocation.Contains(model.txtKeyword));

            return View(datas);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Activity p)
        {
            MingSuContext db = new MingSuContext();
            db.Activities.Add(p);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public IActionResult Delete(int? id)
        {
            if (id != null)
            {
                MingSuContext db = new MingSuContext();
                Activity act = db.Activities.FirstOrDefault(p => p.ActivityId == id);
                if (act != null)
                {
                    db.Activities.Remove(act);
                    db.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }

        public IActionResult Edit(int? id)
        {
            if (id != null)
            {
                MingSuContext db = new MingSuContext();
                Activity act = db.Activities.FirstOrDefault(p => p.ActivityId == id);
                if (act != null)
                    return View(act);
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult Edit(CActivityViewModel inAct)
        {
            MingSuContext db = new MingSuContext();
            Activity act = db.Activities.FirstOrDefault(p => p.ActivityId == inAct.ActivityId);
            if (act != null)
            {                
                act.ActivityName = inAct.ActivityName;
                act.ActivityDate = inAct.ActivityDate;
                act.ActivityCapacity = inAct.ActivityCapacity;
                act.ActivityStatus = inAct.ActivityStatus;
                act.ActivityInfo = inAct.ActivityInfo;
                act.ActivityLocation = inAct.ActivityLocation;

                db.SaveChanges();
            }
            return RedirectToAction("List");
        }
    }
}
