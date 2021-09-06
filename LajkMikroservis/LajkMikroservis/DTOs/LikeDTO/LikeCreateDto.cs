using System;

namespace LajkMikroservis.DTOs.LikeDTO
{
    public class LikeCreateDto
    {
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
