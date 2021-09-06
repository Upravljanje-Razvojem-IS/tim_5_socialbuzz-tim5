using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LajkMikroservis.Entities
{
    public class Like
    {
        /// <summary>
        /// Like Id
        /// </summary>
        [Key]
        public Guid Id { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [Required]
        public int UserId { get; set; }
        /// <summary>
        /// Post id
        /// </summary>
        [Required]
        public int PostId { get; set; }
        /// <summary>
        /// Like tip id
        /// </summary>
        [ForeignKey("LikeTip")]
        public Guid LikeTipId { get; set; }
        /// <summary>
        /// like tip objekat
        /// </summary>
        public LikeTip LikeTip { get; set; }
    }
}
