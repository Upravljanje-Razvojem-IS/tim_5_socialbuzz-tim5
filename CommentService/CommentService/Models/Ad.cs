using System;

namespace CommentService.Models
{
    public class Ad
    {
        /// <summary>
        /// Post uid.
        /// </summary>
        public int  AdId { get; set; }

        /// <summary>
        /// First name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Last name of the user.
        /// </summary>
        public string Content { get; set; }
    }
}
