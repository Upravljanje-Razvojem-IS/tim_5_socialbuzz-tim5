using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.DTOs
{
    public class ItemDto
    {
        /// <summary>
        /// An identifier for the item 
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// Name of the item 
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Description of the item 
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Price of the item 
        /// </summary>
        public String Price { get; set; }

        /// <summary>
        /// Id of the user who adds the item to the wall
        /// </summary>
        public Guid AccountId { get; set; }
    }
}
