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
                new Room{ Code="4123", Type="Code"},
                new Room{ Code="4123", Type="Code"},
                new Room{ Code="", Type="Key"},
                new Room{ Code="", Type="Card"},
                new Room{ Code="", Type="Key"},
                new Room{ Code="", Type="Key"},
                new Room{ Code="", Type="Card"}
            };

            foreach (Room r in rooms)
            {
                context.Rooms.Add(r);
            }
            context.SaveChanges();
        }
    }
}
