using System;

namespace BlockingService.Dtos
{
    public class BlockReadDto
    {
        /// <summary>
        /// Unique identifier for Block entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date and time when Block was created.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Identifier of the User that created Block.
        /// </summary>
        public int BlockedUserId { get; set; }

        /// <summary>
        /// Identifier of the BlockList of Block.
        /// </summary>
        public int BlockListId { get; set; }
    }
}
