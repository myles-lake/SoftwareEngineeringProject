using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SoftwareEngineeringProject.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Department { get; set; }
        public int BannerID { get; set; }
        public string Campus { get; set; }
        public string Room { get; set; }
        public int AssociateDeanID { get; set; }
    }
}
