using BloggingPlatform.Data;
using BloggingPlatform.Dto;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Consumers;

public class GetBlogPostsConsumer : MediatorRequestHandler<GetBlogPosts, Posts>
{
    private readonly BloggingPlatformContext _context;
    
    public GetBlogPostsConsumer(BloggingPlatformContext context)
    {
        _context = context;
    }

    protected override async Task<Posts> Handle(GetBlogPosts request, CancellationToken cancellationToken)
    {
        return await HandleAsync(request, cancellationToken);
    }

    public async Task<Posts> HandleAsync(GetBlogPosts request, CancellationToken cancellationToken)
    {
        var posts = new Posts();

        var result = await _context.BlogPosts.Select(bp => new BlogspotDto
        {
            Id = bp.Id,
            Title = bp.Title,
            Content = bp.Content,
            NumberOfComments = bp.Comments.Count
        }).ToListAsync();

        if (result.Count > 0)
            posts = new Posts { BlogPosts = result };

        return posts;
    }
}
