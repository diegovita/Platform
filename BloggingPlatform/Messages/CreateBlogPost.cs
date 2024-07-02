using BloggingPlatform.Models;
using MassTransit.Mediator;

namespace BloggingPlatform.Messages;

public record CreateBlogPost : Request<BlogPost>
{
    public string Title { get; set; }
    public string Content { get; set; }
}
