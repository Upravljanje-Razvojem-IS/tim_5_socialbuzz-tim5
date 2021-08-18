using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Models
{
    public class ObjectForSaleDTO
    {
        /// <summary>
        /// ID of object for sale
        /// </summary>
        public int ObjectForSaleId { get; set; }

        /// <summary>
        /// Name of object for sale
        /// </summary>
        public string Name { get; set; }
    }
}
