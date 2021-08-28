using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Models
{
    public class BlockMockDto
    {
        /// <summary>
        /// ID of block
        /// </summary>
        public Guid BlockingID { get; set; }


        /// <summary>
        /// Id of the user who blocked another user.
        /// </summary>
        public int BlockerID { get; set; }



        /// <summary>
        /// Id of the blocked user.
        /// </summary>
        public int BlockedID { get; set; }


    }

}
