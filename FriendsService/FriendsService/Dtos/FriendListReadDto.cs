using System.Collections.Generic;

namespace FriendsService.Dtos
{
    public class FriendListReadDto
    {
        /// <summary>
        /// Unique identifier for FriendList entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identifier of the owner User of FriendList.
        /// </summary>
        public int OwnerUserId { get; set; }

        /// <summary>
        /// List of Friends created for this FriendList.
        /// </summary>
        public List<FriendReadDto> Friends { get; set; }
    }
}
