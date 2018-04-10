using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineeringProject.Data;
using SoftwareEngineeringProject.Models;
using Microsoft.Extensions.DependencyInjection;

namespace SoftwareEngineeringProject.Controllers
{
    public class StatusController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IServiceProvider service;

        public StatusController(ApplicationDbContext application, IServiceProvider service)
        {
            _context = application;
            this.service = service;
        }
       
        public IActionResult Index() 
        {
            return View();
        }

        public async Task<IActionResult> Status() {
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var user = await userManager.GetUserAsync(HttpContext.User);
            //user.BannerID;
            var data = _context.KeyRequestLines
                .Where(o=>o.KeyRequest.Requestor == user.BannerID )
                .Include(krl => krl.KeyRequest)
                .AsNoTracking();
            return View(await data.ToListAsync());
        }
    }
}