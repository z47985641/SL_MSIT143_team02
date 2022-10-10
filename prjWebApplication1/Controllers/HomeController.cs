using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PJ_MSIT143_team02.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PJ_MSIT143_team02.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Roomtour()
        {
            return View();
        }
        public IActionResult tour1()
        {
            return View();
        }
        public IActionResult tour2()
        {
            return View();
        }
        public IActionResult tour3()
        {
            return View();
        }
        public IActionResult tour4()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
