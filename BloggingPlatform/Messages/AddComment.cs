namespace BloggingPlatform.Messages;

public class AddComment
{
        public int BlogPostId { get; set; }
        public string Content { get; set; }
}
