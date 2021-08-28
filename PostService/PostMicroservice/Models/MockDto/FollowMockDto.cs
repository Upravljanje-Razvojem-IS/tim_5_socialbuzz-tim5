using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Models
{
    /// <summary>
    /// DTO model for following user
    /// </summary>

    public class FollowMockDto
    {
           /// <summary>
           /// ID of following
           /// </summary>
           public int FollowingID { get; set; }


           /// <summary>
           /// Id of the user who followed the other user
           /// </summary>
           public int FollowerID { get; set; }


            /// <summary>
            /// Id of the user followed by another user
            /// </summary>
            public int FollowedID { get; set; }

           
        }
    
}
