using BloggingPlatform.Consumers;
using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Tests;

public class GetBlogPostConsumerTests
{
    [Fact]
    public async Task Handle_ReturnsBlogPostIfExists()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<BloggingPlatformContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        using (var context = new BloggingPlatformContext(options))
        {
            context.BlogPosts.Add(new BlogPost { Id = 500, Title = "Test", Content = "Content" });
            await context.SaveChangesAsync();
        }
       
        using (var context = new BloggingPlatformContext(options))
        {
            var handler = new GetBlogPostConsumer(context);
            var result = await handler.HandleAsync(new GetBlogPost { Id = 500 }, CancellationToken.None);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Title);
        }
    }

    [Fact]
    public async Task Handle_ReturnsEmptyBlogPostIfNotFound()
    {
    
        var options = new DbContextOptionsBuilder<BloggingPlatformContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
    
        using (var context = new BloggingPlatformContext(options))
        {
            var handler = new GetBlogPostConsumer(context);
            var result = await handler.HandleAsync(new GetBlogPost { Id = 999 }, CancellationToken.None);
           
            Assert.NotNull(result);
            Assert.Equal(default, result.Id);
        }
    }
}

