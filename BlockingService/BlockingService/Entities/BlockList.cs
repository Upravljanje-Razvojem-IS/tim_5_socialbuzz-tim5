using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BlockingService.Entities
{
    public class BlockList
    {
        /// <summary>
        /// Unique identifier for <see cref="BlockList"/> entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the owner <see cref="User"/> of <seealso cref="BlockList"/>.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int OwnerUserId { get; set; }

        /// <summary>
        /// List of <see cref="Block"/>s created for this <seealso cref="BlockList"/>.
        /// </summary>
        public List<Block> Blocks { get; set; }
    }
}
