using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlockingService.Entities
{
    public class Block
    {
        /// <summary>
        /// Unique identifier for <see cref="Block"/> entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Date and time when <see cref="Block"/> was created.
        /// </summary>
        [Required]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Identifier of the <see cref="User"/> that created <seealso cref="Block"/>.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int BlockedUserId { get; set; }

        /// <summary>
        /// Identifier of the <see cref="Entities.BlockList"/> of <seealso cref="Block"/>.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        [ForeignKey("BlockList")]
        public int BlockListId { get; set; }

        /// <summary>
        /// Type object.
        /// </summary>
        public BlockList BlockList { get; set; }
    }
}
