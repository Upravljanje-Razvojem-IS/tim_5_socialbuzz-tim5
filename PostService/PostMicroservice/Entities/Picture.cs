using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace PostMicroservice.Entities
{

    /// <summary>
    /// Entity class which represents image
    /// </summary>
    public class Picture
    {

        /// <summary>
        /// Image ID.
        /// </summary>
        [Key]
        [Required]
        public Guid PictureId { get; set; }

        /// <summary>
        /// Url path of the image.
        /// </summary>
        [Required]
        public string Url { get; set; }

        /// <summary>
        /// ID of the post for which the image was published.
        /// </summary>
        [Required]
        [ForeignKey("PostID")]
        public Guid PostID { get; set; }


    }
}
