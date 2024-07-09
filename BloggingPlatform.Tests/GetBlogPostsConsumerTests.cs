using BloggingPlatform.Consumers;
using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Tests;

public class GetBlogPostsConsumerTests
{
    private DbContextOptions<BloggingPlatformContext> CreateNewContextOptions()
    {
        return new DbContextOptionsBuilder<BloggingPlatformContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    }
    [Fact]
    public async Task Handle_ReturnsPosts_WhenBlogPostsExist()
    {
        var options = CreateNewContextOptions();

        using (var context = new BloggingPlatformContext(options))
        {
            context.BlogPosts.AddRange(
                new BlogPost { Id = 1, Title = "Post 1", Content = "Content 1", Comments = new List<Comment> { new Comment() } },
                new BlogPost { Id = 2, Title = "Post 2", Content = "Content 2", Comments = new List<Comment>() }
            );
            await context.SaveChangesAsync();
        }

        using (var context = new BloggingPlatformContext(options))
        {
            var consumer = new GetBlogPostsConsumer(context);
         
            var result = await consumer.HandleAsync(new GetBlogPosts(), CancellationToken.None);
        
            Assert.NotNull(result);
            Assert.Equal(2, result.BlogPosts.Count);
            Assert.Equal("Post 1", result.BlogPosts[0].Title);
            Assert.Equal("Post 2", result.BlogPosts[1].Title);
        }
    }
}
