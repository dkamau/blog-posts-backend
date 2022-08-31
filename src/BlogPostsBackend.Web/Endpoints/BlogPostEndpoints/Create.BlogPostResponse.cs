using System;
using System.Linq;
using BlogPostsBackend.Core.Entities.BlogPostEntities;

namespace BlogPostsBackend.Web.Endpoints.BlogPostEndpoints
{
    public class BlogPostResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Author { get; set; }

        public static BlogPostResponse Create(BlogPost blogPost)
        {
            BlogPostResponse blogPostResponse = new BlogPostResponse()
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Description = blogPost.Description,
                PublicationDate = blogPost.PublicationDate,
                Author = $"{blogPost.User.FirstName} {blogPost.User.LastName}"
            };

            return blogPostResponse;
        }
    }
}
