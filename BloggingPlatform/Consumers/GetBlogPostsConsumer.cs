using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Consumers
{
    public class GetBlogPostsConsumer : MediatorRequestHandler<GetBlogPosts, Posts>
    {
        private readonly BloggingPlatformContext _context;
        
        public GetBlogPostsConsumer(BloggingPlatformContext context)
        {
            _context = context;
        }

        protected override async Task<Posts> Handle(GetBlogPosts request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _context.BlogPosts
                .Include(bp => bp.Comments)
                .ToListAsync(cancellationToken);

                var posts = new Posts { BlogPosts = result };

                return posts;
            }
            catch(Exception ex)
            {
                throw new ApplicationException("An error occurred while fetching blog posts", ex);
            }
        }
    }
}
