using BloggingPlatform.Data;
using BloggingPlatform.Dto;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;
using Serilog;

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
            var posts = new Posts();

            try
            {
                var result = await _context.BlogPosts.Select(bp => new BlogspotDto
                {
                    Id = bp.Id,
                    Title = bp.Title,
                    Content = bp.Content,
                    NumberOfComments = bp.Comments.Count
                }).ToListAsync();

                if(result.Count > 0)
                    posts = new Posts { BlogPosts = result };

                return posts;
            }
            catch(Exception ex)
            {
                
                throw new ApplicationException("An error occurred while fetching blog posts", ex);
            }
        }
    }
}
