using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.DTOs
{
    public class PastPriceCreationDto
    {
        /// <summary>
        /// Item id to which the previous price applies
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the ItemId.")]
        public Guid ItemId { get; set; }

        /// <summary>
        /// Amount of past price
        /// </summary>
        [Required(ErrorMessage = "It is mandatory to enter the amount of past price.")]
        public String Price { get; set; }
    }
}
