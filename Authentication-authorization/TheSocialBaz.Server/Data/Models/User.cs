using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialBaz.Server.Data.Models
{
    public class User : IdentityUser
    {
        // Extended properties for every user
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsDisabled { get; set; } = false;
    }
}
