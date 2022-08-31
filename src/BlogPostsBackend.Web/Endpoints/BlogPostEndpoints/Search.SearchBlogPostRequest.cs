using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogPostsBackend.Web.Endpoints.BlogPostEndpoints
{
    public class SearchBlogPostRequest
    {
        public string Search { get; set; }
        public string Name { get; set; }
        public int? Page { get; set; }
        public int? RecordsPerPage { get; set; }
        public bool Paginate { get; set; } = true;
    }
}
