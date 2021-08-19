using System.ComponentModel.DataAnnotations;

namespace TheSocialBaz.Server.Models.Identity
{
    public class RegisterCorporationRequestModel
    {
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Name of corporation is required.")]
        public string NameOfCorporation { get; set; }
        [Required(ErrorMessage = "Date when is the corporation founded is required.")]
        public string FoundedAt { get; set; }
    }
}
