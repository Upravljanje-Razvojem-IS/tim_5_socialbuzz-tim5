using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostMicroservice.Entities
{
    /// <summary>
    /// Entity class which represents content of the post
    /// </summary>
    public class Content
    {
        /// <summary>
        /// Content ID.
        /// </summary>
        [Key]
        [Required]
        public Guid ContentId { get; set; }

        /// <summary>
        /// Title of the content.
        /// </summary>
        [StringLength(50)]
        [Required]
        public string Title { get; set; }
        
        /// <summary>
        /// Text of the content.
        /// </summary>
        [StringLength(300)]
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// The possibility of replacing the content that is published.
        /// </summary>
        [Required]
        public bool Replacement { get; set; }

        /// <summary>
        /// The state of the content that is published.
        /// </summary>
        [Required]
        public string State { get; set; }

        /// <summary>
        /// ID of item for sale.
        /// </summary>
        [Required]
        public int ItemForSaleID { get; set; }



    }
}
