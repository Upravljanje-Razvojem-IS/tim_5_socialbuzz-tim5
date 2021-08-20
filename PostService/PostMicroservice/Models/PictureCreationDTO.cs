using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Models
{
    /// <summary>
    /// DTO class which represents image
    /// </summary>
    public class PictureCreationDTO
    {
        /// <summary>
        /// Image ID.
        /// </summary>
        [Key]
        [Required]
        public Guid ImageId { get; set; }

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
        //public Post Post { get; set; }
    }
}
