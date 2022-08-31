using BlogPostsBackend.Core.Interfaces;
using BlogPostsBackend.Core.Services;
using BlogPostsBackend.SharedKernel.Interfaces;
using Moq;

namespace BlogPostsBackend.UnitTests.Core.Services.BlogPostServiceTests.Factory
{
    internal class BlogPostServiceFactory
    {
        internal static BlogPostService Create(
           Mock<IRepository> repository)
        {
            return new BlogPostService(repository.Object);
        }
    }
}
