using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoftwareEngineeringProject.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Rooms.Any())
            {
                return;
            }

            var rooms = new Room[]
            {
                new Room{ Code="4123", Type="Code", RoomID = "E203"},
                new Room{ Code="4123", Type="Code", RoomID = "E204"},
                new Room{ Code="", Type="Key", RoomID = "E205"},
                new Room{ Code="", Type="Card", RoomID = "E206"},
                new Room{ Code="", Type="Key", RoomID = "E207"},
                new Room{ Code="", Type="Key", RoomID = "E208"},
                new Room{ Code="", Type="Card", RoomID = "E209"}
            };


            foreach (Room r in rooms)
            {
                context.Rooms.Add(r);
            }
            context.SaveChanges();
            //Key Request
            var KeyRequest = new KeyRequest[]
            {
                new KeyRequest{ Creation_Date=System.DateTime.Now, Requestor = 370838},
                new KeyRequest{ Creation_Date=System.DateTime.Now, Requestor=846515}

            };
            foreach (KeyRequest r in KeyRequest)
            {
                context.KeyRequest.Add(r);
            }
            context.SaveChanges();

            //Key Request Lines
            var KeyRequestLines = new KeyRequestLines[]
            {
                new KeyRequestLines{ KeyRequestId = 1,ApprovalDate = DateTime.Now,status = "Approved, Waiting to be cut!",Room = "E201",CompletedDate = DateTime.UtcNow},
                new KeyRequestLines{ KeyRequestId = 1,ApprovalDate = DateTime.Now,status = "Waiting for approval",Room = "E201",CompletedDate = DateTime.UtcNow}

            };
            foreach (KeyRequestLines r in KeyRequestLines)
            {
                context.KeyRequestLines.Add(r);
            }
            context.SaveChanges();
        }
    }
}
