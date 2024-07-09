using BloggingPlatform.Consumers;
using BloggingPlatform.Data;
using BloggingPlatform.Messages;
using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace BloggingPlatform.Tests
{
    public class GetTokenConsumerTests
    {
        [Fact]
        public async Task Handle_ReturnsValidTokenForValidUser()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<BloggingPlatformContext>()
                .UseInMemoryDatabase(databaseName: "DbToken")
                .Options;

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("Jwt:Key", "secret_key_secret_key-secret_key_secret_key"),
                    new KeyValuePair<string, string>("Jwt:Issuer", "issuer"),
                    new KeyValuePair<string, string>("Jwt:Audience", "audience")
                })
                .Build();
            
            using (var context = new BloggingPlatformContext(options))
            {
                context.Users.Add(new LoginModel { Username = "user", Password = "password" });
                await context.SaveChangesAsync();
            }

            
            using (var context = new BloggingPlatformContext(options))
            {
                var handler = new GetTokenConsumer(context, configuration);
                var result = await handler.HandleAsync(new GetToken { Username = "user", Password = "password" }, CancellationToken.None);
              
                Assert.NotNull(result);
                Assert.NotEmpty(result.Token);
            }
        }
    }
}

