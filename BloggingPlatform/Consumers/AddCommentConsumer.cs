using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using MassTransit.Mediator;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Consumers;

public class AddCommentConsumer : MediatorRequestHandler<AddComment, Comment>
{
    private readonly BloggingPlatformContext _context;

    public AddCommentConsumer(BloggingPlatformContext context)
    {
        _context = context;
    }
    protected override Task<Comment> Handle(AddComment request, CancellationToken cancellationToken)
    {
        return HandleAsync(request, cancellationToken);
    }
    public async Task<Comment> HandleAsync(AddComment request, CancellationToken cancellationToken)
    {
          var blogPost = await _context.BlogPosts.FirstOrDefaultAsync(x => x.Id == request.BlogPostId);

        if (blogPost is not null)
        {
            var comment = new Comment { BlogPostId = request.BlogPostId, Content = request.Content };

            await _context.Comments.AddAsync(comment);

            await _context.SaveChangesAsync(cancellationToken);

            return comment;
        }
        else
            return new Comment();
    }
}
