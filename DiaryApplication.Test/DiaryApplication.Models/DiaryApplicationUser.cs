using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApplication.Models
{
    // Add profile data for application users by adding properties to the DiaryApplicationUser class
    public class DiaryApplicationUser : IdentityUser
    {
        public string Name { get; set; }

        public string PenName  { get; set; }
    }
}
