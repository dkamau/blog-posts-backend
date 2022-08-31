using System.Collections.Generic;
using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.Core.Services;
using BlogPostsBackend.Core.Specifications.BlogPostSpecifications;
using BlogPostsBackend.SharedKernel.Interfaces;
using BlogPostsBackend.UnitTests.Core.Services.BlogPostServiceTests.Factory;
using Moq;
using Xunit;

namespace BlogPostsBackend.UnitTests.Core.Services.BlogPostServiceTests
{
    public class CountAsync_Should
    {
        [Fact]
        public async Task Call_CountAsync_Method_Once()
        {
            // Arrange
            // Arrange
            Fixture fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            List<BlogPost> blogPosts = fixture.Create<List<BlogPost>>();

            Mock<IRepository> repository = new Mock<IRepository>();
            repository
                .Setup(n => n.CountAsync(It.IsAny<GetBlogPosts>()))
                .Returns(Task.FromResult(blogPosts.Count));

            BlogPostService blogPostService = BlogPostServiceFactory.Create(repository);

            // Act
            var result = await blogPostService.CountAsync(fixture.Create<BlogPostFilter>());

            // Assert
            repository.Verify(n => n.CountAsync(It.IsAny<GetBlogPosts>()), Times.Once, "CountAsync() method to list blogPosts was not called.");
        }

        [Fact]
        public async Task Return_Number_Of_Records()
        {
            // Arrange
            Fixture fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            List<BlogPost> blogPosts = fixture.Create<List<BlogPost>>();

            Mock<IRepository> repository = new Mock<IRepository>();
            repository
                .Setup(n => n.CountAsync(It.IsAny<GetBlogPosts>()))
                .Returns(Task.FromResult(blogPosts.Count));

            BlogPostService blogPostService = BlogPostServiceFactory.Create(repository);

            // Act
            var result = await blogPostService.CountAsync(fixture.Create<BlogPostFilter>());

            // Assert
            Assert.IsType<int>(result);
            Assert.Equal(blogPosts.Count, result);
        }
    }
}
