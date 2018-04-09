using Microsoft.AspNetCore.Identity;
using SoftwareEngineeringProject.Models;
using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Data
{
    public static class DbInitializer
    {
        public static async Task Initialize(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();

            // create users
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            if (await userManager.FindByNameAsync("admin@mohawkcollege.ca") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "admin@mohawkcollege.ca",
                    PhoneNumber = "905-555-5555",
                    UserName = "admin@mohawkcollege.ca",
                    AssociateDeanID = 0,
                    BannerID = 000123123,
                    Campus = "Fennell",
                    Department = "Software Engineering",
                    Room = "Q101"
                };
                await userManager.CreateAsync(user, "Admin1!");
                await userManager.AddToRoleAsync(user, "admin");
            }

            if (await userManager.FindByNameAsync("instructor@mohawkcollege.ca") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "instructor@mohawkcollege.ca",
                    PhoneNumber = "905-555-5555",
                    UserName = "instructor@mohawkcollege.ca",
                    AssociateDeanID = 000123123,
                    BannerID = 000111111,
                    Campus = "Fennell",
                    Department = "Software Engineering",
                    Room = "Q200"
                };
                await userManager.CreateAsync(user, "Instructor1!");
                await userManager.AddToRoleAsync(user, "instructor");
            }

            if (await userManager.FindByNameAsync("security@mohawkcollege.ca") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "security@mohawkcollege.ca",
                    PhoneNumber = "905-555-5555",
                    UserName = "security@mohawkcollege.ca",
                    AssociateDeanID = 0,
                    BannerID = 000222222,
                    Campus = "Fennell",
                    Department = "Security",
                    Room = "C120"
                };
                await userManager.CreateAsync(user, "Security1!");
                await userManager.AddToRoleAsync(user, "security");
            }

            if (await userManager.FindByNameAsync("locksmith@mohawkcollege.ca") == null)
            {
                var user = new ApplicationUser
                {
                    Email = "locksmith@mohawkcollege.ca",
                    PhoneNumber = "905-555-5555",
                    UserName = "locksmith@mohawkcollege.ca",
                    AssociateDeanID = 0,
                    BannerID = 000333333,
                    Campus = "Fennell",
                    Department = "Locksmithing",
                    Room = "B100"
                };
                await userManager.CreateAsync(user, "Locksmith1!");
                await userManager.AddToRoleAsync(user, "locksmith");
            }

            // rooms
            if (!context.Rooms.Any())
            {
                var rooms = new Room[]
                {
                    new Room{ Code="4123", Type="Code", Id = "E203"},
                    new Room{ Code="4123", Type="Code", Id = "E204"},
                    new Room{ Code=null, Type="Key", Id = "E205"},
                    new Room{ Code=null, Type="Card", Id = "E206"},
                    new Room{ Code=null, Type="Key", Id = "E207"},
                    new Room{ Code=null, Type="Key", Id = "E208"},
                    new Room{ Code=null, Type="Card", Id = "E209"}
                };

                foreach (Room r in rooms)
                {
                    context.Rooms.Add(r);
                }

                context.SaveChanges();
            }

            

            //Key Request
            if (!context.KeyRequest.Any())
            {
                var temp = context.Users.FirstOrDefault();
                var KeyRequest = new KeyRequest[]
                {
                    new KeyRequest{ Creation_Date=System.DateTime.Now, Requestor=000123123, UserId=temp.Id},
                    new KeyRequest{ Creation_Date=System.DateTime.Now, Requestor=000123123, UserId=temp.Id}

                };

                foreach (KeyRequest r in KeyRequest)
                {
                    context.KeyRequest.Add(r);
                }

                context.SaveChanges();
            }


            //Key Request Lines
            if (!context.KeyRequestLines.Any())
            {
                var temp = context.KeyRequest.Select(i => i.Id).ToList();

                var KeyRequestLines = new KeyRequestLines[2];
                //{
                //    new KeyRequestLines{ KeyRequestId = 2007, ApprovalDate = DateTime.Now,status = "Approved, Waiting to be cut!",RoomID = "E201",CompletedDate = DateTime.UtcNow},
                //    new KeyRequestLines{ KeyRequestId = 2008,ApprovalDate = DateTime.Now,status = "Waiting for approval",RoomID = "E201",CompletedDate = DateTime.UtcNow}

                //};

                foreach (var item in temp)
                {
                    context.KeyRequestLines.Add(new Data.KeyRequestLines { KeyRequestId = item, ApprovalDate = DateTime.Now, status = "Approved, Waiting to be cut!", RoomID = "E201", CompletedDate = DateTime.UtcNow });
                }

                context.SaveChanges();
            }
        }
    }
}
