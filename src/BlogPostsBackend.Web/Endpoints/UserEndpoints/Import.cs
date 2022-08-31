using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.Core.Exceptions;
using BlogPostsBackend.Core.Interfaces;
using BlogPostsBackend.Web.ApiErrors;
using BlogPostsBackend.Web.Endpoints.BlogPostEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogPostsBackend.Web.Endpoints.UserEndpoints
{
    [Route("User")]
    [AllowAnonymous]
    public class Import : BaseAsyncEndpoint<int, List<BlogPostResponse>>
    {
        protected readonly IImportService _importService;

        public Import(IImportService importService)
        {
            _importService = importService;
        }

        [HttpGet("{id}/BlogPosts/Import")]
        [SwaggerOperation(
            Summary = "Imports a user's BlogPosts",
            Description = "Imports a user's BlogPosts",
            OperationId = "BlogPost.Import",
            Tags = new[] { "User Endpoints" })
        ]
        public override async Task<ActionResult<List<BlogPostResponse>>> HandleAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                List<BlogPost> blogPosts = await _importService.ImportBlogPosts(id);

                return Ok(blogPosts.Select(n => BlogPostResponse.Create(n)).OrderBy(n => n.Title).OrderByDescending(n => n.PublicationDate).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestError(ex.Message, ExceptionCode.InternalServerError.ToString()));
            }
        }
    }
}
