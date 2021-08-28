using System;

namespace FriendsService.Dtos
{
    public class FriendReadDto
    {
        /// <summary>
        /// Unique identifier for Friend entity.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date and time when Friend was created.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Identifier of the User that created Friend.
        /// </summary>
        public int FriendUserId { get; set; }

        /// <summary>
        /// Identifier of the FriendList of Friend.
        /// </summary>
        public int FriendListId { get; set; }
    }
}
