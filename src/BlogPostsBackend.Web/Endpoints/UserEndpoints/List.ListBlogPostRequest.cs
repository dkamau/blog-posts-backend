﻿namespace BlogPostsBackend.Web.Endpoints.UserEndpoints
{
    public class ListBlogPostRequest
    {
        public int? Page { get; set; }
        public int? RecordsPerPage { get; set; }
        public bool Paginate { get; set; } = true;
    }
}
