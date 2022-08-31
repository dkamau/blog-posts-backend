using System;
using System.Collections.Generic;
using System.Linq;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.Web.AbstractClasses;
using BlogPostsBackend.Web.Endpoints.BlogPostEndpoints;

namespace BlogPostsBackend.Web.Endpoints.UserEndpoints
{
    public class ListBlogPostResponse : Pagination
    {
        public ListBlogPostResponse(List<BlogPost> blogPosts, int? page, int? recordsPerPage, int totalRecords)
        {
            Page = page == null ? 1 : (int)page;
            RecordsPerPage = recordsPerPage == null ? blogPosts.Count : (int)recordsPerPage;
            TotalRecords = totalRecords;
            TotalPages = TotalRecords > 0 ? (int)Math.Ceiling(TotalRecords / (double)RecordsPerPage) : 0;

            BlogPosts = blogPosts.Select(n => BlogPostResponse.Create(n)).ToList();

            RecordsInPage = BlogPosts.Count;

            if (Page > 1)
                PreviousPage = Page - 1;
            if (Page > TotalPages)
                PreviousPage = TotalPages;
            if (PreviousPage == 0)
                PreviousPage = null;
            if (Page < TotalPages)
                NextPage = Page + 1;
        }

        public List<BlogPostResponse> BlogPosts { get; set; }
    }
}
