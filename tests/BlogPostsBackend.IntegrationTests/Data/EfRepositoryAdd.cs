using System.Linq;
using System.Threading.Tasks;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.UnitTests;
using Xunit;

namespace BlogPostsBackend.IntegrationTests.Data
{
    public class EfRepositoryAdd : BaseEfRepoTestFixture
    {
        [Fact]
        public async Task AddsItemAndSetsId()
        {
            var repository = GetRepository();
            var item = new BlogPostBuilder().Build();

            await repository.AddAsync(item);

            var newItem = (await repository.ListAsync<BlogPost>())
                            .FirstOrDefault();

            Assert.Equal(item, newItem);
            Assert.True(newItem?.Id > 0);
        }
    }
}
