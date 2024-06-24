using System;

namespace CommentService.Models
{
    public class Account
    {
        /// <summary>
        /// Account uid.
        /// </summary>
        public Guid AccountUid { get; set; }

        /// <summary>
        /// First name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user.
        /// </summary>
        public string LastName { get; set; }
    }
}
