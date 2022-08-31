using System.Collections.Generic;
using System.Threading.Tasks;
using BlogPostsBackend.Core.Entities.BlogPostEntities;

namespace BlogPostsBackend.Core.Interfaces
{
    public interface IImportService
    {
        Task<List<BlogPost>> ImportBlogPosts(int userId);
    }
}
