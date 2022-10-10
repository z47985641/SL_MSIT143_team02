using Microsoft.AspNetCore.Mvc;
using PJ_MSIT143_team02.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PJ_MSIT143_team02.Models;

namespace PJ_MSIT143_team02.Controllers
{
    public class MemberController : Controller
    {
        public IActionResult MemberList(CMenberSearch KW)
        {
            MingSuContext db = new MingSuContext();
            IEnumerable<Member> datas = from I in db.Members
                                        join A in db.Cities
                                        on I.CityId equals A.CityId
                                        select I;

            if (!string.IsNullOrEmpty(KW.KW_MemberAccount))
                datas = datas.Where(p => p.MemberAccount.Contains(KW.KW_MemberAccount));
            if (!string.IsNullOrEmpty(KW.KW_MemberPassword))
                datas = datas.Where(p => p.MemberPassword.Contains(KW.KW_MemberPassword));
            if (!string.IsNullOrEmpty(KW.KW_MemberName))
                datas = datas.Where(p => p.MemberName.Contains(KW.KW_MemberName));
            if (!string.IsNullOrEmpty(KW.KW_MemberPhone))
               datas = datas.Where(p => p.MemberPhone.Contains(KW.KW_MemberPhone));
            if (!string.IsNullOrEmpty(KW.KW_MemberEmail))
               datas = datas.Where(p => p.MemberEmail.Contains(KW.KW_MemberEmail));
            if (KW.KW_CityId >0)
               datas = datas.Where(p => p.CityId.Equals(KW.KW_CityId));
            if (!string.IsNullOrEmpty(KW.KW_Authority))
               datas = datas.Where(p => p.Authority.Contains(KW.KW_Authority));

            return View(datas);
        }
        public IActionResult MemberEdit(int? MemberId)
        {
            MingSuContext db = new MingSuContext();
            Member datas = db.Members.FirstOrDefault(Member => Member.MemberId == MemberId);
            return View(datas);
        }
        [HttpPost]
        public IActionResult MemberEdit(Member datasedit)
        {
            MingSuContext db = new MingSuContext();
            Member datas = db.Members.FirstOrDefault(Member => Member.MemberId == datasedit.MemberId);
            datas.MemberEmail       = datasedit.MemberEmail   ;
            datas.MemberAccount     = datasedit.MemberAccount ;
            datas.MemberName        = datasedit.MemberName    ;
            datas.MemberPassword    = datasedit.MemberPassword;
            datas.MemberPhone       = datasedit.MemberPhone   ;
            datas.Authority         = datasedit.Authority     ;
            datas.Admins             = datasedit.Admins         ;
            datas.BirthDate         = datasedit.BirthDate;
            db.SaveChanges();

            return RedirectToAction("MemberList");
        }
        public IActionResult MemberDelete(int? MemberId)
        {
            MingSuContext db = new MingSuContext();
            Member c = db.Members.FirstOrDefault(t => t.MemberId == MemberId);
            if (c != null)
            {
                db.Members.Remove(c);
                db.SaveChanges();
            }
            return RedirectToAction("MemberList");
        }
        
        public IActionResult MemberCreate()
        {
            return View();
        }
        [HttpPost]
        public IActionResult MemberCreate(Member member)
        {
            MingSuContext db = new MingSuContext();
            db.Members.Add(member);
            db.SaveChanges();
            return RedirectToAction("MemberList");
        }
    }
}
