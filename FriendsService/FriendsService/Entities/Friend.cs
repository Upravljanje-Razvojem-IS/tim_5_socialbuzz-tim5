using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FriendsService.Entities
{
    public class Friend
    {
        /// <summary>
        /// Unique identifier for <see cref="Friend"/> entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Date and time when <see cref="Friend"/> was created.
        /// </summary>
        [Required]
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Identifier of the <see cref="User"/> that created <seealso cref="Friend"/>.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int FriendUserId { get; set; }

        /// <summary>
        /// Identifier of the <see cref="Entities.FriendList"/> of <seealso cref="Friend"/>.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        [ForeignKey("FriendList")]
        public int FriendListId { get; set; }

        /// <summary>
        /// Type object.
        /// </summary>
        public FriendList FriendList { get; set; }
    }
}
