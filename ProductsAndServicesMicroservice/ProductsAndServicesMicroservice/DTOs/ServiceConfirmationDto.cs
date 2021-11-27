using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsAndServicesMicroservice.DTOs
{
    public class ServiceConfirmationDto
    {
        /// <summary>
        /// An identifier for the service
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// Name of the service
        /// </summary>
        public String Name { get; set; }

        /// <summary>
        /// Description of the service
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// Price of the service
        /// </summary>
        public String Price { get; set; }

        /// <summary>
        /// Id of the user who adds the service to the wall
        /// </summary>
        public Guid AccountId { get; set; }

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
