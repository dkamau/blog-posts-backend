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
    public class ListAsync_Should
    {
        [Fact]
        public async Task Call_ListAsync_Method_Once()
        {
            // Arrange
            // Arrange
            Fixture fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            List<BlogPost> blogPosts = fixture.Create<List<BlogPost>>();

            Mock<IRepository> repository = new Mock<IRepository>();
            repository
                .Setup(n => n.ListAsync(It.IsAny<GetBlogPosts>()))
                .Returns(Task.FromResult(blogPosts));

            BlogPostService blogPostService = BlogPostServiceFactory.Create(repository);

            // Act
            var result = await blogPostService.ListAsync(fixture.Create<BlogPostFilter>());

            // Assert
            repository.Verify(n => n.ListAsync(It.IsAny<GetBlogPosts>()), Times.Once, "ListAsync() method to list blogPosts was not called.");
        }

        [Fact]
        public async Task Return_A_List_Of_Records()
        {
            // Arrange
            Fixture fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            List<BlogPost> blogPosts = fixture.Create<List<BlogPost>>();

            Mock<IRepository> repository = new Mock<IRepository>();
            repository
                .Setup(n => n.ListAsync(It.IsAny<GetBlogPosts>()))
                .Returns(Task.FromResult(blogPosts));

            BlogPostService blogPostService = BlogPostServiceFactory.Create(repository);

            // Act
            var result = await blogPostService.ListAsync(fixture.Create<BlogPostFilter>());

            // Assert
            Assert.IsType<List<BlogPost>>(result);
            Assert.NotNull(result);
        }
    }
}
