using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheSocialBaz.Server.Models.Identity
{
    public class RegisterUserRequestModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string DoB { get; set; }
        public string PhoneNumber { get; set; }
    }
}
