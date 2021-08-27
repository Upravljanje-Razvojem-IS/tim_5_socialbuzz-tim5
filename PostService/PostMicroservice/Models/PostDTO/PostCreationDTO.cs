using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Models.PostDto
{

    /// <summary>
    /// DTO class which represents post.
    /// </summary>
    public class PostCreationDto
    {
        /// <summary>
        /// Content of the post.
        /// </summary>
        [Required(ErrorMessage = "You must enter id of the content for post.")]
        [ForeignKey("ContentID")]
        public Guid ContentId { get; set; }

        /// <summary>
        /// Id of the user who posted the post.
        /// </summary>
        [Required(ErrorMessage = "You must enter id of the user.")]
        public int UserId { get; set; }
    }
}
