namespace FriendsService.Dtos
{
    public class FriendWithUserDto : FriendReadDto
    {
        public UserReadDto User { get; set; }
    }
}
