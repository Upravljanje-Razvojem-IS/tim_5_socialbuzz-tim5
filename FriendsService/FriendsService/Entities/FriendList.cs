using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FriendsService.Entities
{
    public class FriendList
    {
        /// <summary>
        /// Unique identifier for <see cref="FriendList"/> entity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the owner <see cref="User"/> of <seealso cref="FriendList"/>.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int OwnerUserId { get; set; }

        /// <summary>
        /// List of <see cref="Friend"/>s created for this <seealso cref="FriendList"/>.
        /// </summary>
        public List<Friend> Friends { get; set; }
    }
}
