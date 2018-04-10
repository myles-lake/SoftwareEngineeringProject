﻿using System;
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
               .Include(krl => krl.Room)
               .Include(krl => krl.KeyRequest)
                   .ThenInclude(kr => kr.ApplicationUser)
                            .Where(l => l.KeyRequest.ApplicationUser.AssociateDeanID == user.BannerID
                            && l.ApprovalDate == null);

            

            return View(await data.ToListAsync());
        }
    }
}