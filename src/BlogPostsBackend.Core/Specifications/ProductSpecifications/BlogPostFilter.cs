using BlogPostsBackend.Core.Entities.BlogPostEntities;

namespace BlogPostsBackend.Core.Specifications.BlogPostSpecifications
{
    public class BlogPostFilter : BlogPost
    {
        public string Search { get; set; }
        public int? Page { get; set; }
        public int? RecordsPerPage { get; set; }
    }
}
