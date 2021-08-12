using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.MockDTOs
{
    /// <summary>
    /// DTO model za korisnike
    /// </summary>
    public class UserMockDTO
    {

        /// <summary>
        /// ID korisnika
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Username korisnika
        /// </summary>
        public String Username { get; set; }


        /// <summary>
        /// Password korisnika
        /// </summary>
        public String Password { get; set; }

        /// <summary>
        /// Email korisnika
        /// </summary>
        public String Email { get; set; }

        /// <summary>
        /// Da li je korisnikov nalog aktivan
        /// </summary>
        public Boolean IsActive { get; set; }

        /// <summary>
        /// Telefon korisnika
        /// </summary>
        public String Telephone { get; set; }

        /// <summary>
        /// Datum kreiranja naloga korisnika
        /// </summary>
        public DateTime DateOfCreation { get; set; }


    }
}
