using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.Core.Exceptions;
using BlogPostsBackend.Core.Services;
using BlogPostsBackend.Core.Specifications.BlogPostSpecifications;
using BlogPostsBackend.SharedKernel.Interfaces;
using BlogPostsBackend.UnitTests.Core.Services.BlogPostServiceTests.Factory;
using Moq;
using Xunit;

namespace BlogPostsBackend.UnitTests.Core.Services.BlogPostServiceTests
{
    public class GetByIdAsync_Should
    {
        [Fact]
        public async Task Call_FirstOrDefaultAsync_Method_Once()
        {
            // Arrange
            BlogPost blogPost = GenerateExistingBlogPost();

            Mock<IRepository> repository = new Mock<IRepository>();
            repository
                .Setup(n => n.FirstOrDefaultAsync(It.IsAny<GetBlogPosts>()))
                .Returns(Task.FromResult(blogPost));

            BlogPostService blogPostService = BlogPostServiceFactory.Create(repository);

            // Act
            var result = await blogPostService.GetByIdAsync(blogPost.Id);

            // Assert
            repository.Verify(n => n.FirstOrDefaultAsync(It.IsAny<GetBlogPosts>()), Times.Once);
        }

        [Fact]
        public async Task Return_A_Single_Valid_Record()
        {
            // Arrange
            BlogPost blogPost = GenerateExistingBlogPost();

            Mock<IRepository> repository = new Mock<IRepository>();
            repository
                .Setup(n => n.FirstOrDefaultAsync(It.IsAny<GetBlogPosts>()))
                .Returns(Task.FromResult(blogPost));

            BlogPostService blogPostService = BlogPostServiceFactory.Create(repository);

            // Act
            var result = await blogPostService.GetByIdAsync(blogPost.Id);

            // Assert
            Assert.IsType<BlogPost>(result);
            Assert.Equal(blogPost.Id, result.Id);
        }

        [Theory()]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task Throw_CustomException_If_Id_Is_InvalidId(int id)
        {
            // Arrange
            BlogPostService blogPostService = BlogPostServiceFactory.Create(new Mock<IRepository>());

            // Act
            // Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => blogPostService.GetByIdAsync(id));
            Assert.Equal(ExceptionCode.InvalidBlogPostId.ToString(), exception.ExceptionCode);
        }

        [Fact]
        public async Task Throw_CustomException_If_Record_Is_NotFound()
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
            var exception = await Assert.ThrowsAsync<CustomException>(() => blogPostService.GetByIdAsync(blogPostId));
            Assert.Equal(ExceptionCode.BlogPostNotFound.ToString(), exception.ExceptionCode);
        }

        private BlogPost GenerateExistingBlogPost()
        {
            Fixture fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            BlogPost blogPost = fixture.Create<BlogPost>();

            return blogPost;
        }
    }
}
