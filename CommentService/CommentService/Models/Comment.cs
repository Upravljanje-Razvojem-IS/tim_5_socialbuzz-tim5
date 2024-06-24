using System;

namespace CommentService.Models
{
    public class Comment
    {
        /// <summary>
        /// Comment id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Uid of the account that commented.
        /// </summary>
        public Guid AccountUid { get; set; }

        /// <summary>
        /// Ad id of the ad that was commented.
        /// </summary>
        public int AdId { get; set; }

        /// <summary>
        /// Comment content.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// DateTime when this Account was created.
        /// </summary>
        public DateTime TimeAndDate { get; set; }
    }
}
