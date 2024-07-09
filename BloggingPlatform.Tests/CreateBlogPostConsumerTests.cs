using BloggingPlatform.Consumers;
using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Tests;

public class CreateBlogPostConsumerTests
{
    [Fact]
    public async Task Handle_CreateNewBlogPost()
    {
     
        var options = new DbContextOptionsBuilder<BloggingPlatformContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        using (var context = new BloggingPlatformContext(options))
        {
            var handler = new CreateBlogPostConsumer(context);
            var result = await handler.HandleAsync(new CreateBlogPost { Title = "New Blog Post", Content = "Content" }, CancellationToken.None);

           
            Assert.NotNull(result);
            Assert.Equal("New Blog Post", result.Title);
            Assert.Equal("Content", result.Content);
        }
    }
}

