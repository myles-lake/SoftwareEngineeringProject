using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SoftwareEngineeringProject.Models;

namespace SoftwareEngineeringProject.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);            
        }
        // makes the room table
        public DbSet<Room> Rooms { get; set; }
        public DbSet<KeyRequestLines> KeyRequestLines { get; set; }
        public DbSet<KeyRequest> KeyRequest { get; set; }
        // makes the room table

        public DbSet<SoftwareEngineeringProject.Models.ApplicationUser> ApplicationUser { get; set; }
    }

    // sets up the rooms table and all of its propertoes
    public class Room
    {
        public string Id { get; set; }
        [MaxLength(4)]
        public string Code { get; set; }
        [MaxLength(10)]
        public string Type { get; set; }

        
        public KeyRequestLines KeyRequestLines { get; set; }
    }


    public class KeyRequest
    {
        public int Id { get; set; }
        
        public DateTime Creation_Date { get; set; }
        
        public int Requestor { get; set; }

        [ForeignKey("KeyRequestId")]
        public ICollection<KeyRequestLines> KeyRequestLines { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        public string UserId { get; set; }
    }



    public class KeyRequestLines
    {
        public int Id { get; set; }

        [ForeignKey("KeyRequestId")]
        public KeyRequest KeyRequest { get; set; }
        public int KeyRequestId { get; set; }
        
        public System.Nullable<DateTime> CompletedDate { get; set; }

        public System.Nullable<DateTime> ApprovalDate { get; set; }

        public string status { get; set; }
        
        public Room Room { get; set; }
        public string RoomID { get; set; }

        public string ReasonForAccess { get; set; }
        public string Campus { get; set; }

    }


}
