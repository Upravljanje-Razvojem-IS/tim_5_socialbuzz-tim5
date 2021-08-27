using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostMicroservice.Entities
{

    /// <summary>
    /// An identifier for the user account
    /// </summary>
    public class UserAccountDto
    {
           /// <summary>
           /// ID of user account
           /// </summary>
           public int UserAccountId { get; set; }

            /// <summary>
            /// Username of the user account
            /// </summary>
            public string UserName { get; set; }

         
        
    }
}
