using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AlausDaryklosGamybosValdymoSistema.Models;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace AlausDaryklosGamybosValdymoSistema.Controllers
{
    public class HomeController : Controller
    {
        ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
       
        public IActionResult Index()
        {
            return View();
        }
       /* public IActionResult Redirect()
        {
            return RedirectToAction("Create", "KlientoUzsakymoAplankasController");
        }
        public IActionResult Redirect2()
        {
            return RedirectToAction("ArchyvuotiUzsakymai", "KlientoUzsakymoAplankas");
        }*/
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
