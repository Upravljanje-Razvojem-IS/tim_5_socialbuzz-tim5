using System.ComponentModel.DataAnnotations;

namespace FriendsService.Dtos
{
    public class FriendCreateDto
    {
        /// <summary>
        /// Identifier of the User that created Friend.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int FriendUserId { get; set; }

        /// <summary>
        /// Identifier of the FriendList of Friend.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int FriendListId { get; set; }
    }
}
