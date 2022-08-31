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
    public class RecordExistsAsync_Should
    {
        [Theory()]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task Return_False_If_Id_Is_Invalid(int id)
        {
            // Arrange
            BlogPostService blogPostService = BlogPostServiceFactory.Create(new Mock<IRepository>());

            // Act
            // Assert
            var result = await blogPostService.RecordExistsAsync(id);
            Assert.False(result);
        }

        [Fact]
        public async Task Return_False_If_Record_DoesNotExist()
        {
            // Arrange
            int blogPostId = 1;

            Mock<IRepository> repository = new Mock<IRepository>();
            repository
                .Setup(n => n.FirstOrDefaultAsync(It.IsAny<GetBlogPosts>()))
                .Returns(Task.FromResult((BlogPost)null));

            BlogPostService blogPostService = BlogPostServiceFactory.Create(repository);

            // Act
            // Assert
            var result = await blogPostService.RecordExistsAsync(blogPostId);
            Assert.False(result);
        }

        [Fact]
        public async Task Return_True_If_Record_Exists()
        {
            // Arrange
            int blogPostId = 1;

            Fixture fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            BlogPost blogPost = fixture.Build<BlogPost>().With(n => n.Id, blogPostId).Create();

            Mock<IRepository> repository = new Mock<IRepository>();
            repository
                .Setup(n => n.FirstOrDefaultAsync(It.IsAny<GetBlogPosts>()))
                .Returns(Task.FromResult(blogPost));

            BlogPostService blogPostService = BlogPostServiceFactory.Create(repository);

            // Act
            // Assert
            var result = await blogPostService.RecordExistsAsync(blogPostId);
            Assert.True(result);
        }
    }
}
