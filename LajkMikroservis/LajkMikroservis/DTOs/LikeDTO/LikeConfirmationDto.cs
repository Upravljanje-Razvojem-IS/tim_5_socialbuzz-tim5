using System;

namespace LajkMikroservis.DTOs.LikeDTO
{
    public class LikeConfirmationDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Post id
        /// </summary>
        public int PostId { get; set; }
        /// <summary>
        /// Like tip id
        /// </summary>
        public Guid LikeTipId { get; set; }
    }
}
