using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.DTOs
{
    public class ProductConfirmationDto
    {
        /// <summary>
        /// An identifier for the product
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// Name of the product
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Description of the product
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        public String Price { get; set; }

        /// <summary>
        /// Id of the user who adds the product to the wall
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Weight of the product
        /// </summary>
        public String Weight { get; set; }

        /// <summary>
        /// Quantity of the product
        /// </summary>
        public String Quantity { get; set; }

        /// <summary>
        /// Production date of the product
        /// </summary>
        public DateTime ProductionDate { get; set; }
    }

}
