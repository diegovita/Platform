﻿using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Consumers;

public class GetBlogPostConsumer : MediatorRequestHandler<GetBlogPost, BlogPost?>
{
    private readonly BloggingPlatformContext _context;
    public GetBlogPostConsumer(BloggingPlatformContext context)
    {
        _context = context;
    }

    protected override Task<BlogPost?> Handle(GetBlogPost request, CancellationToken cancellationToken)
    {
        return HandleAsync(request, cancellationToken);
    }

    public async Task<BlogPost?> HandleAsync(GetBlogPost request, CancellationToken cancellationToken)
    {
        var blogPost = await _context.BlogPosts.Include(bp => bp.Comments)
                                               .FirstOrDefaultAsync(bp => bp.Id == request.Id, cancellationToken);

        if (blogPost is null)
            return new BlogPost();
        
        return blogPost;
    }
}
