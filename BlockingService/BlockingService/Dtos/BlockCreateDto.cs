using System.ComponentModel.DataAnnotations;

namespace BlockingService.Dtos
{
    public class BlockCreateDto
    {
        /// <summary>
        /// Identifier of the User that created Block.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int BlockedUserId { get; set; }

        /// <summary>
        /// Identifier of the BlockList of Block.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int BlockListId { get; set; }
    }
}
