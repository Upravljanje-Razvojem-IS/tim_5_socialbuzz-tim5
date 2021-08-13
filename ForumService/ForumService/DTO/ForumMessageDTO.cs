using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.DTO
{
    /// <summary>
    /// DTO model poruke u forumu
    /// </summary>
    public class ForumMessageDTO
    {
        /// <summary>
        /// ID poruke
        /// </summary>
        public int ForumMessageID { get; set; }

        /// <summary>
        /// ID foruma gde se nalazi poruka
        /// </summary>
        public int ForumID { get; set; }

        /// <summary>
        /// Id posiljaoca
        /// </summary>
        public int SenderID { get; set; }

        /// <summary>
        /// Naslov poruke
        /// </summary>
        public String Title { get; set; }

        /// <summary>
        /// Tekst poruke
        /// </summary>
        public String Body { get; set; }
    }
}
