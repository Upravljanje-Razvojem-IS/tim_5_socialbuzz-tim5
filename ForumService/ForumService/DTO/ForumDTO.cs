using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.DTO
{
    /// <summary>
    /// DTO model foruma
    /// </summary>
    public class ForumDTO
    {
        /// <summary>
        /// ID foruma
        /// </summary>
        public int ForumID { get; set; }

        /// <summary>
        /// ID korisnika koji je vlasnik foruma
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Ime foruma
        /// </summary>
        public String ForumName { get; set; }

        /// <summary>
        /// Opis foruma
        /// </summary>
        public String ForumDescription { get; set; }

        /// <summary>
        /// Link slike
        /// </summary>
        public String LogoLink { get; set; }

        /// <summary>
        /// Da li je forum otvoren da slanje poruka
        /// </summary>
        public bool IsOpen { get; set; }
    }
}
