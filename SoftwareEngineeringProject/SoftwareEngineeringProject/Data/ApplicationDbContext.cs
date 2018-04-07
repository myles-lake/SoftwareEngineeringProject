using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        // makes the room table
        public DbSet<SoftwareEngineeringProject.Models.ApplicationUser> ApplicationUser { get; set; }
    }

    // sets up the rooms table and all of its propertoes
    public class Room
    {
        public int Id { get; set; }
        [MaxLength(4)]
        public string Code { get; set; }
        [MaxLength(10)]
        public string Type { get; set; }
    }
}
