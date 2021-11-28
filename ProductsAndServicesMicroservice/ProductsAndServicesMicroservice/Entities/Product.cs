using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Entities

/// <summary>
/// Entity class which represents product
/// </summary>
{
    public class Product : Item
    {

        /// <summary>
        /// Production date of the product
        /// </summary>
        public DateTime ProductionDate { get; set; }

        /// <summary>
        /// Weight of the product
        /// </summary>
        public String Weight { get; set; }

        /// <summary>
        /// Quantity of the product
        /// </summary>
        public String Quantity { get; set; }
    }
}
