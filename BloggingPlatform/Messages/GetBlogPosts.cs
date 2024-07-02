using BloggingPlatform.Models;
using MassTransit.Mediator;

namespace BloggingPlatform.Messages;


// Posts is used instead of List<BlogPost> to satisfy Masstransit condition
public record GetBlogPosts : Request<Posts> { }
