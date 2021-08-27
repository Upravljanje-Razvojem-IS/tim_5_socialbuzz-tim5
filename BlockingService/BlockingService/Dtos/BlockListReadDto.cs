using System.Collections.Generic;

namespace BlockingService.Dtos
{
    public class BlockListReadDto
    {
        /// <summary>
        /// Unique identifier for BlockList entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the owner User of BlockList.
        /// </summary>
        public int OwnerUserId { get; set; }

        /// <summary>
        /// List of Blocks created for this BlockList.
        /// </summary>
        public List<BlockReadDto> Blocks { get; set; }
    }
}
