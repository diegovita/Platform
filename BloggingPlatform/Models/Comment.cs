using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BloggingPlatform.Models;

public class Comment
{
    public int Id { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public int BlogPostId { get; set; }
    [JsonIgnore]
    public BlogPost BlogPost { get; set; }
}
