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
        private UserManager<ApplicationUser> userManager; 
        public ApproveController(ApplicationDbContext application, IServiceProvider service)
        {
            _context = application;
            this.service = service;
            userManager = service.GetService<UserManager<ApplicationUser>>();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Approve()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var data = _context.KeyRequestLines
               .Include(krl => krl.Room)
               .Include(krl => krl.KeyRequest)
                   .ThenInclude(kr => kr.ApplicationUser)
                            .Where(l => l.KeyRequest.ApplicationUser.AssociateDeanID == user.BannerID
                                     && l.ApprovalDate == null);

            var isLocksmith = await userManager.IsInRoleAsync(user, "locksmith");
            var isSecurity = await userManager.IsInRoleAsync(user, "security");
            if (isLocksmith)
            {
                data = _context.KeyRequestLines
                   .Include(krl => krl.Room)
                   .Include(krl => krl.KeyRequest)
                       .ThenInclude(kr => kr.ApplicationUser)
                                .Where(l => l.CompletedDate == null
                                         && l.Room.Type == "Key");
            }
            if (isSecurity)
            {
                data = _context.KeyRequestLines
                   .Include(krl => krl.Room)
                   .Include(krl => krl.KeyRequest)
                       .ThenInclude(kr => kr.ApplicationUser)
                                .Where(l => l.CompletedDate == null
                                         && (l.Room.Type == "Code"
                                         || l.Room.Type == "Card"));
            }



            return View(await data.AsNoTracking().ToListAsync());
        }

        public async Task<IActionResult> ApproveKey(int id, string type)
        {
            var entity = _context.KeyRequestLines
                            .FirstOrDefault(krl => krl.Id == id);

            var user = await userManager.GetUserAsync(HttpContext.User);
            if (entity != null)
            {
                var isLocksmith = await userManager.IsInRoleAsync(user, "locksmith");
                var isSecurity = await userManager.IsInRoleAsync(user, "security");
                var isAdmin = await userManager.IsInRoleAsync(user, "admin");
                if (isAdmin)
                {
                    entity.ApprovalDate = DateTime.Now;
                    if (type == "Key")
                    {
                        entity.status = "Awaiting to be cut";
                    }
                    else
                    {
                        entity.status = "Permissions being approved";
                    }
                }
                if (isLocksmith)
                {
                    entity.CompletedDate = DateTime.Now;
                    entity.status = "Approved, key cut";
                }
                if (isSecurity)
                {
                    entity.CompletedDate = DateTime.Now;
                    entity.status = "Approved, permissions applied";
                }
                _context.KeyRequestLines.Update(entity);

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Approve", "Approve");
        }

        public async Task<IActionResult> DissapproveKey(int id)
        {
            var entity = _context.KeyRequestLines.FirstOrDefault(krl => krl.Id == id);
            if (entity != null)
            {
                entity.ApprovalDate = DateTime.Now;
                entity.status = "Dissapproved";
                _context.KeyRequestLines.Update(entity);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Approve", "Approve");
        }
    }
}