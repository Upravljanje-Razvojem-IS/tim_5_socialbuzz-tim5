using System.ComponentModel.DataAnnotations;

namespace TheSocialBaz.Server.Models.Identity
{
    public class RegisterUserRequestModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Surname is required.")]
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string DoB { get; set; }
        public string PhoneNumber { get; set; }
    }
}
