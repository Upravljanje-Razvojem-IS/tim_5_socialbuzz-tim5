using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Entities
{

    /// <summary>
    /// Entity class which represents model where is store last price of item for sale
    /// </summary>
    public class PastPrice
    {
        /// <summary>
        /// An identifier for the past price
        /// </summary>
        [Key]
        [Required]
        public int PastPriceId { get; set; }

        /// <summary>
        /// Item id to which the previous price applies
        /// </summary>
        [ForeignKey("ItemId")]
        public Guid ItemId { get; set; }

        /// <summary>
        /// Amount of past price
        /// </summary>
        [Required]
        public String Price { get; set; }
    }
}
