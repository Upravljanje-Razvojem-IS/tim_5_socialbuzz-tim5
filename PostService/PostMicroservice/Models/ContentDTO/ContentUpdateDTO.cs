using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Models.ContentDTO
{
    /// <summary>
    /// DTO class which represents model of content for update.
    /// </summary>
    public class ContentUpdateDTO
    {

        /// <summary>
        /// Content ID.
        /// </summary>
        [Key]
        [Required(ErrorMessage = "You must enter id of the content.")]
        public Guid ContentId { get; set; }

        /// <summary>
        /// Title of the content.
        /// </summary>
        [StringLength(50)]
        [Required(ErrorMessage = "You must enter title of the content.")]
        public string Title { get; set; }

        /// <summary>
        /// Text of the content.
        /// </summary>
        [StringLength(300)]
        [Required(ErrorMessage = "You must enter text of the content.")]
        public string Text { get; set; }

        /// <summary>
        /// The possibility of replacing the content that is published.
        /// </summary>
        [Required(ErrorMessage = "You must enter possibility of replacing.")]
        public bool Replacement { get; set; }

        /// <summary>
        /// The state of the content that is published.
        /// </summary>
        [Required(ErrorMessage = "You must enter state.")]
        public string State { get; set; }

        /// <summary>
        /// ID of item for sale.
        /// </summary>
        [Required(ErrorMessage = "You must enter object for sale.")]
        public int ItemForSaleID { get; set; }

    }
}
