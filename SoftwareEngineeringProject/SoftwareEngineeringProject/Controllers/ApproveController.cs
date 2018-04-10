using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SoftwareEngineeringProject.Controllers
{
    [Authorize(Roles = "admin,security,locksmith")]
    public class ApproveController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Approve() {

            return View();
        }
    }
}