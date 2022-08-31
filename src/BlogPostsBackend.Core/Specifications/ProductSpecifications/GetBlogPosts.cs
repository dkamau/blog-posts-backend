using Ardalis.Specification;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using Microsoft.EntityFrameworkCore;

namespace BlogPostsBackend.Core.Specifications.BlogPostSpecifications
{
    public class GetBlogPosts : Specification<BlogPost>
    {
        public GetBlogPosts(BlogPostFilter filter)
        {
            if (filter.Id > 0)
                Query.Where(n => n.Id == filter.Id);

            if (filter.UserId > 0)
                Query.Where(n => n.UserId == filter.UserId);

            if (!string.IsNullOrEmpty(filter.Title))
                Query.Where(n => n.Title.ToLower() == filter.Title.ToLower());

            if (!string.IsNullOrEmpty(filter.Search))
                Query.Where(n =>
                EF.Functions.Like(n.Title.ToLower(), $"%{filter.Search.ToLower()}%") ||
                EF.Functions.Like(n.Description.ToLower(), $"%{filter.Search.ToLower()}%"));

            Query.Include(n => n.User);
        }
    }
}
