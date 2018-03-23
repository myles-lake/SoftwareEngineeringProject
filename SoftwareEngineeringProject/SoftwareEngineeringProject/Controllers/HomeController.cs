using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Models;

namespace SoftwareEngineeringProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Approve() {

            return View();
        }

        public IActionResult Lookup() {
            ViewData["Message"] = "Your contact page.";
            

            return View();
        }

        public IActionResult RequestKey() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Status() {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
