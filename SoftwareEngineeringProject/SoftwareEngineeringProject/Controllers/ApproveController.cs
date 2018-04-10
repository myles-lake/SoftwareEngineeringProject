using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoftwareEngineeringProject.Data;
using SoftwareEngineeringProject.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace SoftwareEngineeringProject.Controllers
{
    [Authorize(Roles = "admin,security,locksmith")]
    public class ApproveController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider service;
        public ApproveController(ApplicationDbContext application, IServiceProvider service)
        {
            _context = application;
            this.service = service;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Approve()
        {
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.GetUserAsync(HttpContext.User);
            var data = _context.KeyRequestLines
                .Where(l => l.KeyRequest.ApplicationUser.AssociateDeanID == user.BannerID)
               .Include(krl => krl.Room)
               .Include(krl => krl.KeyRequest)
                   .ThenInclude(kr => kr.ApplicationUser);
               //.Where(l=>l.KeyRequest.ApplicationUser.AssociateDeanID.ToString()==user.BannerID.ToString());
            //Where(krl => krl.KeyRequest.ApplicationUser.BannerID.ToString().Contains(searchString));
            /*var data = _context.KeyRequestLines
                .Join(
                _context.KeyRequest,
                krl => krl.KeyRequestId,
                kr => kr.Id,
                (krl, kr) => new { KeyRequestLines = krl, KeyRequest = kr }
                )
                .Join(_context.Users,
                u => u.KeyRequest.Requestor,
                us=> us.BannerID,
                (u, us) => new { KeyRequest = u, Users = us }
                )
                .Where(l=>l.Users.AssociateDeanID ==user.BannerID)
                .Select(
                c=>new{
                    c.KeyRequest.KeyRequest.Requestor,
                    c.Users.Email,
                    c.KeyRequest.KeyRequestLines.RoomID,
                    c.KeyRequest.KeyRequestLines.Room.Type,
                    c.KeyRequest.KeyRequestLines.Id
                });*/

            /*.(u=>u.-
            .Where(o => o. == user.AssociateDeanID)
            .AsNoTracking();*/

            return View(await data.ToListAsync());
        }
    }
}