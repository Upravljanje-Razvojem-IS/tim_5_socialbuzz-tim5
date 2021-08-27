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
        public Guid BlockingID;


        /// <summary>
        /// Id of the user who blocked another user.
        /// </summary>
        public int BlockerID;



        /// <summary>
        /// Id of the blocked user.
        /// </summary>
        public int BlockedID;

       
    }

}
