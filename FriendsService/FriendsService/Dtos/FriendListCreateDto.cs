using System.ComponentModel.DataAnnotations;

namespace FriendsService.Dtos
{
    public class FriendListCreateDto
    {
        /// <summary>
        /// Identifier of the owner User of FriendList.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int OwnerUserId { get; set; }
    }
}
