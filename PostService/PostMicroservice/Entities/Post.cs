using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PostMicroservice.Entities
{
    /// <summary>
    /// Entity class which represents post
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Post ID
        /// </summary>
        [Key]
        [Required]
        public Guid PostId { get; set; }

        /// <summary>
        /// Date and time publication of the post.
        /// </summary>
        [Required]
        public DateTime DateOfPublication { get; set; }

        /// <summary>
        /// Date and time of publication expiration.
        /// </summary>
        [Required]
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Content of the post.
        /// </summary>
        [Required]
        [ForeignKey("ContentID")]
        public Guid ContentId { get; set; }

        /// <summary>
        /// Id of the user who posted the post.
        /// </summary>
        [Required]
        public int UserId { get; set; }

    }
}
