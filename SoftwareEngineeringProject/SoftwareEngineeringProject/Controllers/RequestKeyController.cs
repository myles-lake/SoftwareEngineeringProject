using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Data;
using Microsoft.Extensions.DependencyInjection;
using SoftwareEngineeringProject.Models;
using Microsoft.AspNetCore.Identity;

namespace SoftwareEngineeringProject.Controllers
{
    public class RequestKeyController : Controller
    {
        ApplicationDbContext applicationDbContext;
        IServiceProvider serviceProvider;

        public RequestKeyController(ApplicationDbContext applicationDbContext, IServiceProvider serviceProvider)
        {
            this.applicationDbContext = applicationDbContext;
            this.serviceProvider = serviceProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RequestKey()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ProcessKeyRequests(IFormCollection form)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.GetUserAsync(HttpContext.User);

            string[] campus = form["campus"];
            string[] roomNumber = form["roomNumber"];
            string[] reasonForAccess = form["reasonForAccess"];

            var keyRequest = new KeyRequest
            {
                Creation_Date = DateTime.Now,
                Requestor = user.BannerID
            };

            applicationDbContext.KeyRequest.Add(keyRequest);
            applicationDbContext.SaveChanges();

            for (int i = 0; i < campus.Length; i++)
            {
                var keyRequestLine = new KeyRequestLines
                {
                    KeyRequestId = applicationDbContext.KeyRequest
                        .Where(k => k.Requestor == user.BannerID)
                        .Where(c => c.Creation_Date == keyRequest.Creation_Date)
                        .FirstOrDefault()
                        .Id,
                    RoomID = roomNumber[i],
                    status = "waiting for approval",
                    ReasonForAccess = reasonForAccess[i],
                    Campus = campus[i],
                    ApprovalDate = null,
                    CompletedDate = null
                };
                applicationDbContext.KeyRequestLines.Add(keyRequestLine);
                await applicationDbContext.SaveChangesAsync();
            }

            return View("RequestKey");
        }
    }
}