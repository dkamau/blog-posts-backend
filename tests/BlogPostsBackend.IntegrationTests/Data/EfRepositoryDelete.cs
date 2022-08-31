using System;
using System.Threading.Tasks;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.UnitTests;
using Xunit;

namespace BlogPostsBackend.IntegrationTests.Data
{
    public class EfRepositoryDelete : BaseEfRepoTestFixture
    {
        [Fact]
        public async Task DeletesItemAfterAddingIt()
        {
            // add an item
            var repository = GetRepository();
            var initialTitle = Guid.NewGuid().ToString();
            var item = new BlogPostBuilder().Title(initialTitle).Build();
            await repository.AddAsync(item);

            // delete the item
            await repository.DeleteAsync(item);

            // verify it's no longer there
            Assert.DoesNotContain(await repository.ListAsync<BlogPost>(),
                i => i.Title == initialTitle);
        }
    }
}
