using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.ViewModels;
using PJ_MSIT143_team02.Models;
using PJ_MSIT143_team02.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class MemberCreateController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(Member member)
        {
            MingSuContext db = new MingSuContext();
            db.Members.Add(member);
            db.SaveChanges();

            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(CLoginViewModel vModel)
        {
            Member cust = (new MingSuContext()).Members.FirstOrDefault(c => c.MemberAccount.Equals(vModel.txtAccount));
            if (cust != null)
            {
                if (cust.MemberPassword.Equals(vModel.txtPassword))
                {
                    string jsonUser = JsonSerializer.Serialize(cust);
                    HttpContext.Session.SetString("KK", jsonUser);
                    ViewBag.check = true;

                    if(cust.Authority.Equals("管理員"))
                        return RedirectToAction("AdminMainPage", "MemberOnly");

                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
        }
    }
}
