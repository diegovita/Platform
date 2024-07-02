using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit.Mediator;

namespace BloggingPlatform.Consumers
{
    public class CreateBlogPostConsumer : MediatorRequestHandler<CreateBlogPost, BlogPost>
    {
        private readonly BloggingPlatformContext _context;

        public CreateBlogPostConsumer(BloggingPlatformContext context)
        {
            _context = context;
        }
        protected override async Task<BlogPost> Handle(CreateBlogPost request, CancellationToken cancellationToken)
        {
            var blogPost = new BlogPost { Content = request.Content, Title = request.Title };

           await _context.BlogPosts.AddAsync(blogPost);

            await _context.SaveChangesAsync();

            return blogPost;
        }
    }
}
