using System.ComponentModel.DataAnnotations;

namespace TheSocialBaz.Server.Models.Identity
{
    public class RegisterCorporationRequestModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string NameOfCorporation { get; set; }
        [Required]
        public string FoundedAt { get; set; }
    }
}
