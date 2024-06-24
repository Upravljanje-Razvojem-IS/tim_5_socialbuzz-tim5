namespace CommentService.DTOs
{
    public class CommentUpdateDto
    {
        /// <summary>
        /// Id of an Account.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Comment content.
        /// </summary>
        public string Text { get; set; }
    }
}
