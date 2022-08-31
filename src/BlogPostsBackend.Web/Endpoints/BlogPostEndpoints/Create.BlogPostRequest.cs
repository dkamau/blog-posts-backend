using System;
using System.ComponentModel.DataAnnotations;

namespace BlogPostsBackend.Web.Endpoints.BlogPostEndpoints
{
    public class BlogPostRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
    }
}
