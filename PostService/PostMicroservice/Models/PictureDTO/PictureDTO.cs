using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Models
{
    /// <summary>
    /// DTO class which represents picture.
    /// </summary>
    public class PictureDto
    {
        /// <summary>
        /// Picture ID.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "You must enter id of the picture.")]
        public Guid PictureId { get; set; }

        /// <summary>
        /// Url path of the picture.
        /// </summary>
        [Required(ErrorMessage = "You must enter url path of the picture.")]
        public string Url { get; set; }

        /// <summary>
        /// ID of the post for which the picture was published.
        /// </summary>
        [Required(ErrorMessage = "You must enter post for the picture.")]
        [ForeignKey("PostID")]
        public Guid PostID { get; set; }

    }
}
