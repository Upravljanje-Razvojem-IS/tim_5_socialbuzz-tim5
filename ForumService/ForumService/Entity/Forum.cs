using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ForumService.Entity
{
    /// <summary>
    /// Model foruma
    /// </summary>
    [Table("Forum")]
    public class Forum
    {
        /// <summary>
        /// ID foruma
        /// </summary>
        [Key]
        [Required]
        public int ForumID { get; set; }

        /// <summary>
        /// ID korisnika koji je vlasnik foruma
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Ime foruma
        /// </summary>
        public String ForumName { get; set; }

        ///// <summary>
        ///// Poruke u forumu
        ///// </summary>
        //public List<ForumMessage> ForumMessages { get; set; }

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
