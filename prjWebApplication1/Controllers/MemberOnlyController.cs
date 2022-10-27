using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class MemberOnlyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminMainPage()
        {
            return View();
        }
        public IActionResult MemnerMainPage()
        {
            return View();
        }
        
    }
}
