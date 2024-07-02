using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Consumers
{
    public class GetBlogPostConsumer : MediatorRequestHandler<GetBlogPost, BlogPost?>
    {
        private readonly BloggingPlatformContext _context;
        public GetBlogPostConsumer(BloggingPlatformContext context)
        {
            _context = context;
        }
        protected override async Task<BlogPost?> Handle(GetBlogPost request, CancellationToken cancellationToken)
        {


            var blogPost = await _context.BlogPosts.Include(bp => bp.Comments)
                                                   .FirstOrDefaultAsync(bp => bp.Id == request.Id, cancellationToken);

            if(blogPost is null)
                throw new KeyNotFoundException($"Blog post with ID: {request.Id} not found");
            
            return blogPost;
        }
    }
}
