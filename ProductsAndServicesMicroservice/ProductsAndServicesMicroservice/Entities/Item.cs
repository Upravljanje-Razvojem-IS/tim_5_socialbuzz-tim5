using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Entities

/// <summary>
/// Entity class which represents items that could be products or services
/// </summary>
{
    public class Item
    {
        /// <summary>
        /// An identifier for the item 
        /// </summary>
        [Key]
        [Required]
        public Guid ItemId { get; set; }

        /// <summary>
        /// Name of the item 
        /// </summary>
        [StringLength(50)]
        [Required]
        public String Name { get; set; }

        /// <summary>
        /// Description of the item 
        /// </summary>
        [StringLength(300)]
        [Required]
        public String Description { get; set; }

        /// <summary>
        /// Price of the item 
        /// </summary>
        [Required]
        public String Price { get; set; }

        /// <summary>
        /// Id of the user who adds the item to the wall
        /// </summary>
        public Guid AccountId { get; set; }
    }
}
