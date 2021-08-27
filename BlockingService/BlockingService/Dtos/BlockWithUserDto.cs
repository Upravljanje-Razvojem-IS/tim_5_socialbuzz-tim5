namespace BlockingService.Dtos
{
    public class BlockWithUserDto : BlockReadDto
    {
        public UserReadDto User { get; set; }
    }
}
