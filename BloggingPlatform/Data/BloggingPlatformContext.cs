using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Data;

public class BloggingPlatformContext :  DbContext
{
    public BloggingPlatformContext(DbContextOptions<BloggingPlatformContext> options) : base(options) { }
    public DbSet<BlogPost> BlogPosts { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<LoginModel> Users { get; set; }
}
