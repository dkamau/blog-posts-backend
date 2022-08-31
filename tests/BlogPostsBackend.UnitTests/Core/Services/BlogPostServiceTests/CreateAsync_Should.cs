using System.Threading.Tasks;
using AutoFixture;
using AutoFixture.AutoMoq;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.Core.Exceptions;
using BlogPostsBackend.Core.Interfaces;
using BlogPostsBackend.Core.Services;
using BlogPostsBackend.Core.Specifications.BlogPostSpecifications;
using BlogPostsBackend.SharedKernel.Interfaces;
using BlogPostsBackend.UnitTests.Core.Services.BlogPostServiceTests.Factory;
using Moq;
using Xunit;

namespace BlogPostsBackend.UnitTests.Core.Services.BlogPostServiceTests
{
    public class CreateAsync_Should
    {
        [Fact]
        public async Task Call_AddAsync_Method_Once()
        {
            // Arrange
            BlogPost blogPost = GenerateNewBlogPost();

            Mock<IRepository> repository = MockIRepositoryWithAddAsyncReturning(blogPost);

            BlogPostService blogPostService = BlogPostServiceFactory.Create(repository);

            // Act
            var result = await blogPostService.CreateAsync(blogPost);

            // Assert
            repository.Verify(n => n.AddAsync(It.IsAny<BlogPost>()), Times.Once, "AddAsync() method to add item to the database was not called.");
        }

        [Fact]
        public async Task Add_A_Record_To_The_Database_And_Return_It()
        {
            // Arrange
            BlogPost blogPost = GenerateNewBlogPost();
            BlogPost createdBlogPost = new BlogPost()
            {
                Id = 1,
            };

            Mock<IRepository> repository = MockIRepositoryWithAddAsyncReturning(createdBlogPost);

            BlogPostService blogPostService = BlogPostServiceFactory.Create(repository);

            // Act
            var result = await blogPostService.CreateAsync(blogPost);

            // Assert
            Assert.IsType<BlogPost>(result);
            Assert.True(result.Id > 0, "BlogPost Id should be greater than 0 after successful addition to the database.");
        }


        [Fact]
        public async Task Throw_CustomException_If_Record_AlreadyExists()
        {
            // Arrange
            BlogPost blogPost = GenerateNewBlogPost();
            BlogPost existingBlogPost = new BlogPost()
            {
                Id = 1,
                Title = blogPost.Title
            };

            Mock<IRepository> repository = new Mock<IRepository>();
            repository
               .Setup(n => n.FirstOrDefaultAsync(It.IsAny<GetBlogPosts>()))
               .Returns(Task.FromResult(existingBlogPost));

            BlogPostService blogPostService = BlogPostServiceFactory.Create(repository);

            // Act
            // Assert
            var exception = await Assert.ThrowsAsync<CustomException>(() => blogPostService.CreateAsync(blogPost));
            Assert.Equal(ExceptionCode.BlogPostAlreadyExists.ToString(), exception.ExceptionCode);
        }

        private Fixture GetFixture()
        {
            Fixture fixture = new Fixture();
            fixture.Customize(new AutoMoqCustomization());
            return fixture;
        }

        private BlogPost GenerateNewBlogPost()
        {
            Fixture fixture = GetFixture();
            BlogPost blogPost = fixture.Build<BlogPost>()
                .With(n => n.Id, 0).Create();

            return blogPost;
        }

        private Mock<IRepository> MockIRepositoryWithAddAsyncReturning(BlogPost blogPost)
        {
            Mock<IRepository> repository = new Mock<IRepository>();
            repository
                .Setup(n => n.AddAsync(It.IsAny<BlogPost>()))
                .Returns(Task.FromResult(blogPost));

            return repository;
        }
    }
}
