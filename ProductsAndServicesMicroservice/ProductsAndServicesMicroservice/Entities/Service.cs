using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.Entities
{

    /// <summary>
    /// Entity class which represents service
    /// </summary>
    public class Service : Item
    {
        /// <summary>
        /// Start date of the service
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date of the service
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
