namespace PorukaService.DTOs
{
    public class MessageCreateDto
    {
        /// <summary>
        /// Message content
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// Sender id
        /// </summary>
        public int SenderId { get; set; }
        /// <summary>
        /// ReciverId
        /// </summary>
        public int ReciverId { get; set; }
        /// <summary>
        /// is seen check
        /// </summary>
        public bool IsSeen { get; set; }
    }
}
