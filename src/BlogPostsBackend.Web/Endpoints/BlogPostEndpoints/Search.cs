using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.Core.Exceptions;
using BlogPostsBackend.Core.Interfaces;
using BlogPostsBackend.Core.Specifications.BlogPostSpecifications;
using BlogPostsBackend.Web.ApiErrors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogPostsBackend.Web.Endpoints.BlogPostEndpoints
{
    [AllowAnonymous]
    [Route("/BlogPosts")]
    public class Search : BaseAsyncEndpoint<SearchBlogPostRequest, BlogPostResponse>
    {
        private readonly ICrudService<BlogPost> _blogPostService;

        public Search(ICrudService<BlogPost> blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet]
        [SwaggerOperation(
            Summary = "Get's a search of BlogPosts",
            Description = "Get's a search of BlogPosts",
            OperationId = "BlogPost.Search",
            Tags = new[] { "BlogPost Endpoints" })
        ]
        public override async Task<ActionResult<BlogPostResponse>> HandleAsync([FromQuery] SearchBlogPostRequest request, CancellationToken cancellationToken)
        {
            try
            {
                BlogPostFilter blogPostFilter = new BlogPostFilter()
                {
                    Search = request.Search,
                    Title = request.Name,
                };
                int totalRecords = await _blogPostService.CountAsync(blogPostFilter);

                blogPostFilter.Page = request.Page;
                blogPostFilter.RecordsPerPage = request.RecordsPerPage;
                List<BlogPost> blogPosts = await _blogPostService.ListAsync(blogPostFilter);


                if (request.Paginate)
                    return Ok(new ListBlogPostResponse(blogPosts.OrderBy(n => n.Title).OrderByDescending(n => n.PublicationDate).ToList(), request.Page, request.RecordsPerPage, totalRecords));
                else
                    return Ok(blogPosts.OrderBy(n => n.Title).OrderByDescending(n => n.PublicationDate).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestError(ex.Message, ExceptionCode.InternalServerError.ToString()));
            }
        }
    }
}
