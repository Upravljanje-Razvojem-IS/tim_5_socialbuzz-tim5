using System.ComponentModel.DataAnnotations;

namespace BlockingService.Dtos
{
    public class BlockListCreateDto
    {
        /// <summary>
        /// Identifier of the owner User of BlockList.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int OwnerUserId { get; set; }
    }
}
