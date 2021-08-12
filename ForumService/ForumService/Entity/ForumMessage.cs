using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Entity
{
    /// <summary>
    /// Model poruke na forumu
    /// </summary>
    [Table("ForumMessage")]
    public class ForumMessage
    {
        /// <summary>
        /// ID poruke
        /// </summary>
        [Key]
        [Required]
        public int ForumMessageID { get; set; }

        /// <summary>
        /// ID foruma gde se nalazi poruka
        /// </summary>
        public int ForumID { get; set; }
        public Forum Forum { get; set; }

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
