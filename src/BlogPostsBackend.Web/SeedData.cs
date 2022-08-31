using System;
using System.Linq;
using BlogPostsBackend.Core.Entities.AuthenticationEntities;
using BlogPostsBackend.Core.Services;
using BlogPostsBackend.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogPostsBackend.Web
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>(), null))
            {
                // Look for any TODO items.
                PopulateTestData(dbContext);
            }
        }
        public static void PopulateTestData(AppDbContext dbContext)
        {
            if(!dbContext.Users.Any())
            {
                AuthenticationService authenticationService = new AuthenticationService(null, null, null);
                User user = new User()
                {
                    FirstName = "Blog",
                    LastName = "Admin",
                    EmailIsConfirmed = true,
                    PhoneNumberIsConfirmed = true,
                    PhoneNumber = "(+254) 700 000000",
                    Email = "admin@square1.com",
                };

                byte[] passwordHash, passwordSalt;
                authenticationService.CreatePasswordHash("Square1@254!", out passwordHash, out passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;

                dbContext.Add(user);
                dbContext.SaveChanges();
            }
        }
    }
}
