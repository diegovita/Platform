using BloggingPlatform.Models;
using MassTransit.Mediator;
using System.ComponentModel.DataAnnotations;

namespace BloggingPlatform.Messages;

public record CreateBlogPost : Request<BlogPost>
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
}
