using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.Models;

public class BlogPost
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
    public List<Comment> Comments { get; set; } = [];
}
