using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineeringProject.Data;
using SoftwareEngineeringProject.Models;

namespace SoftwareEngineeringProject.Controllers
{
    // checks that the user is admin before running the controller
    [Authorize(Roles = "admin")]
    public class LookupController : Controller 
    {

        private readonly ApplicationDbContext _context;

        public LookupController(ApplicationDbContext context) {
            _context = context;
        }

        public IActionResult Index() 
        {
            return View();
        }

        public async Task<IActionResult> Lookup(string sortOrder, string searchString) {
            ViewData["IDSortParam"] = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewData["DeptSortParam"] = sortOrder == "Dept" ? "dept_desc" : "Dept";
            ViewData["EmailSortParam"] = sortOrder == "Email" ? "email_desc" : "Email";
            ViewData["RoomSortParam"] = sortOrder == "Room" ? "room_desc" : "Room";
            ViewData["TypeSortParam"] = sortOrder == "Type" ? "type_desc" : "Type";
            ViewData["ReasonSortParam"] = sortOrder == "Reason" ? "reason_desc" : "Reason";
            ViewData["CreationSortParam"] = sortOrder == "Creation" ? "creation_desc" : "Creation";
            ViewData["ApprovedSortParam"] = sortOrder == "Approved" ? "approved_desc" : "Approved";
            ViewData["CompletedSortParam"] = sortOrder == "Completed" ? "completed_desc" : "Completed";
            ViewData["StatusSortParam"] = sortOrder == "Status" ? "status_desc" : "Status";
            ViewData["CurrentFilter"] = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                var data = _context.KeyRequestLines
                     .Include(krl => krl.Room)
                     .Include(krl => krl.KeyRequest)
                         .ThenInclude(kr => kr.ApplicationUser)
                      .Where(krl => krl.KeyRequest.ApplicationUser.BannerID.ToString().Contains(searchString)
                            || krl.KeyRequest.ApplicationUser.Department.Contains(searchString)
                            || krl.KeyRequest.ApplicationUser.Email.Contains(searchString)
                            || krl.RoomID.Contains(searchString)
                            || krl.Room.Type.Contains(searchString)
                            || krl.ReasonForAccess.Contains(searchString)
                            || krl.KeyRequest.Creation_Date.ToString().Contains(searchString)
                            || krl.ApprovalDate.ToString().Contains(searchString)
                            || krl.CompletedDate.ToString().Contains(searchString)
                            || krl.status.Contains(searchString));
                return View(await data.AsNoTracking().ToListAsync());
            }
            else
            {
                var data = _context.KeyRequestLines
                       .Include(krl => krl.Room)
                       .Include(krl => krl.KeyRequest)
                           .ThenInclude(kr => kr.ApplicationUser);
                return View(await data.AsNoTracking().ToListAsync());
            }


            //switch (sortOrder)
            //{
            //    case "dept_desc":
            //        var deptD = data.OrderByDescending(o => o.KeyRequest.ApplicationUser.Department);
            //        return View(await deptD.AsNoTracking().ToListAsync());
            //    case "Dept":
            //        var deptA = data.OrderBy(o => o.KeyRequest.ApplicationUser.Department);
            //        return View(await deptA.AsNoTracking().ToListAsync());

            //    case "email_desc":
            //        var emailD = data.OrderByDescending(o => o.KeyRequest.ApplicationUser.Email);
            //        return View(await emailD.AsNoTracking().ToListAsync());
            //    case "Email":
            //        var emailA = data.OrderBy(o => o.KeyRequest.ApplicationUser.Email);
            //        return View(await emailA.AsNoTracking().ToListAsync());

            //    case "room_desc":
            //        var roomD = data.OrderByDescending(o => o.RoomID);
            //        return View(await roomD.AsNoTracking().ToListAsync());
            //    case "Room":
            //        var roomA = data.OrderBy(o => o.RoomID);
            //        return View(await roomA.AsNoTracking().ToListAsync());

            //    case "type_desc":
            //        var typeD = data.OrderByDescending(o => o.Room.Type);
            //        return View(await typeD.AsNoTracking().ToListAsync());
            //    case "Type":
            //        var typeA = data.OrderBy(o => o.Room.Type);
            //        return View(await typeA.AsNoTracking().ToListAsync());

            //    case "reason_desc":
            //        var reasonD = data.OrderByDescending(o => o.ReasonForAccess);
            //        return View(await reasonD.AsNoTracking().ToListAsync());
            //    case "Reason":
            //        var reasonA = data.OrderBy(o => o.ReasonForAccess);
            //        return View(await reasonA.AsNoTracking().ToListAsync());

            //    case "creation_desc":
            //        var creationD = data.OrderByDescending(o => o.KeyRequest.Creation_Date);
            //        return View(await creationD.AsNoTracking().ToListAsync());
            //    case "Creation":
            //        var creationA = data.OrderBy(o => o.KeyRequest.Creation_Date);
            //        return View(await creationA.AsNoTracking().ToListAsync());

            //    case "approved_desc":
            //        var approvedD = data.OrderByDescending(o => o.KeyRequest.Creation_Date);
            //        return View(await approvedD.AsNoTracking().ToListAsync());
            //    case "Approved":
            //        var approvedA = data.OrderBy(o => o.KeyRequest.Creation_Date);
            //        return View(await approvedA.AsNoTracking().ToListAsync());

            //    case "completed_desc":
            //        var completedD = data.OrderByDescending(o => o.KeyRequest.Creation_Date);
            //        return View(await completedD.AsNoTracking().ToListAsync());
            //    case "Completed":
            //        var completedA = data.OrderBy(o => o.KeyRequest.Creation_Date);
            //        return View(await completedA.AsNoTracking().ToListAsync());
            //    case "status_desc":
            //        var statusD = data.OrderByDescending(o => o.KeyRequest.Creation_Date);
            //        return View(await statusD.AsNoTracking().ToListAsync());
            //    case "Status":
            //        var statusA = data.OrderBy(o => o.KeyRequest.Creation_Date);
            //        return View(await statusA.AsNoTracking().ToListAsync());

            //    case "id_desc":
            //        var id = data.OrderByDescending(o => o.KeyRequest.ApplicationUser.BannerID);
            //        return View(await id.AsNoTracking().ToListAsync());
            //    default:
            //        var def = data.OrderBy(o => o.KeyRequest.ApplicationUser.BannerID);
            //        return View(await def.AsNoTracking().ToListAsync());
            //}
        }
    }
}