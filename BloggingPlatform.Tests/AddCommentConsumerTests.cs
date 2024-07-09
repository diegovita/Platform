using BloggingPlatform.Consumers;
using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Tests;

public class AddCommentConsumerTests
{
    [Fact]
    public async Task Handle_AddCommentToBlogPost()
    {
        var options = new DbContextOptionsBuilder<BloggingPlatformContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        using (var context = new BloggingPlatformContext(options))
        {
            var blogPostId = 1;
            context.BlogPosts.Add(new BlogPost { Id = blogPostId, Title = "Blog Post", Content = "Content" });
            await context.SaveChangesAsync();
        }
     
        using (var context = new BloggingPlatformContext(options))
        {
            var handler = new AddCommentConsumer(context);
            var result = await handler.HandleAsync(new AddComment { BlogPostId = 1, Content = "Comment" }, CancellationToken.None);
           
            Assert.NotNull(result);
            Assert.Equal("Comment", result.Content);
            Assert.Equal(1, result.BlogPostId);
        }
    }

    [Fact]
    public async Task Handle_ReturnsEmptyCommentForNonExistingBlogPost()
    {
      
        var options = new DbContextOptionsBuilder<BloggingPlatformContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
      
        using (var context = new BloggingPlatformContext(options))
        {
            var handler = new AddCommentConsumer(context);
            var result = await handler.HandleAsync(new AddComment { BlogPostId = 999, Content = "Comment" }, CancellationToken.None);
          
            Assert.NotNull(result);
            Assert.Equal(default, result.Id);
        }
    }
}


