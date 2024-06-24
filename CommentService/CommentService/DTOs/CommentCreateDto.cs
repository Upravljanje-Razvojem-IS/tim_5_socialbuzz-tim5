using System;

namespace CommentService.DTOs
{
    public class CommentCreateDto
    {
        /// <summary>
        /// Uid of the account that commented.
        /// </summary>
        public Guid AccountUid { get; set; }

        /// <summary>
        /// Ad id of the post that was commented.
        /// </summary>
        public int AdId { get; set; }

        /// <summary>
        /// Comment content.
        /// </summary>
        public string Text { get; set; }
    }
}
