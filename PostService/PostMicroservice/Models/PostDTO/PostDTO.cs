using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Models.PostDTO
{
    /// <summary>
    /// DTO class which represents post.
    /// </summary>
    public class PostDTO
    {

        /// <summary>
        /// Post ID
        /// </summary>
        [Key]
        [Required(ErrorMessage = "You must enter id of the post.")]
        public Guid PostId { get; set; }

        /// <summary>
        /// Date and time publication of the post.
        /// </summary>
        public DateTime DateOfPublication { get; set; }

        /// <summary>
        /// Date and time of publication expiration.
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Content of the post.
        /// </summary>
        [Required(ErrorMessage = "You must enter id of the content for post.")]
        [ForeignKey("ContentID")]
        public Guid ContentId { get; set; }
        //public Content Content { get; set; }

        /// <summary>
        /// Id of the user who posted the post.
        /// </summary>
        [Required(ErrorMessage = "You must enter id of the user.")]
        public int UserId { get; set; }
    }
}
