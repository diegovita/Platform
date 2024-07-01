namespace BloggingPlatform.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; }
    public int BlogPostId { get; set; }
    public BlogPost BlogPost { get; set; }
}
