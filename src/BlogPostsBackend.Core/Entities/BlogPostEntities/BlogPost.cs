using System;
using BlogPostsBackend.Core.Entities.AuthenticationEntities;
using BlogPostsBackend.SharedKernel;

namespace BlogPostsBackend.Core.Entities.BlogPostEntities
{
    public class BlogPost : BaseEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
    }
}
