using BloggingPlatform.Models;
using Microsoft.EntityFrameworkCore;

namespace BloggingPlatform.Data;

public static class DbInitializer
{
    public static void Initialize(BloggingPlatformContext context)
    {
        context.Database.EnsureCreated();

        if (context.Users.Any())
            return;  
     
        var defaultUser = new LoginModel
        {
            Username = "Blogging",
            Password = "Platform",
        };

        context.Users.Add(defaultUser);
        context.SaveChanges();
    }
}

