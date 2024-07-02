using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit.Mediator;

namespace BloggingPlatform.Consumers
{
    public class AddCommentConsumer : MediatorRequestHandler<AddComment, Comment>
    {
        private readonly BloggingPlatformContext _context;

        public AddCommentConsumer(BloggingPlatformContext context)
        {
            _context = context;
        }
        protected override async Task<Comment> Handle(AddComment request, CancellationToken cancellationToken)
        {
            var comment = new Comment { BlogPostId = request.BlogPostId, Content = request.Content };

            await _context.Comments.AddAsync(comment);

            await _context.SaveChangesAsync(cancellationToken);

            return comment;
        }
    }
}
