using BloggingPlatform.Models;
using MassTransit.Mediator;

namespace BloggingPlatform.Messages;

public record GetBlogPost : Request<BlogPost?>
{
    public int Id { get; set; }
}
