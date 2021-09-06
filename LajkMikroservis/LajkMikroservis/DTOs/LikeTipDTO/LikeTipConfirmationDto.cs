using System;

namespace LajkMikroservis.DTOs.LikeTipDTO
{
    public class LikeTipConfirmationDto
    {
        /// <summary>
        /// Like tip id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Tip lajka
        /// </summary>
        public string Tip { get; set; }
    }
}
