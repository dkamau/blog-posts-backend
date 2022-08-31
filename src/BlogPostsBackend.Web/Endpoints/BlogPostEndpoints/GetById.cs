using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.Core.Exceptions;
using BlogPostsBackend.Core.Interfaces;
using BlogPostsBackend.Web.ApiErrors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogPostsBackend.Web.Endpoints.BlogPostEndpoints
{
    [AllowAnonymous]
    [Route("/BlogPosts")]
    public class GetById : BaseAsyncEndpoint<int, BlogPostResponse>
    {
        private readonly ICrudService<BlogPost> _blogPostService;

        public GetById(ICrudService<BlogPost> blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpGet("{id:int}")]
        [SwaggerOperation(
            Summary = "Get's a single BlogPost by Id",
            Description = "Get's a single BlogPost by Id",
            OperationId = "BlogPost.GetById",
            Tags = new[] { "BlogPost Endpoints" })
        ]
        public override async Task<ActionResult<BlogPostResponse>> HandleAsync(int id, CancellationToken cancellationToken)
        {
            try
            {
                BlogPost blogPost = await _blogPostService.GetByIdAsync(id);
                return Ok(BlogPostResponse.Create(blogPost));
            }
            catch (CustomException ex)
            {
                return new CustomErrorResuslt().Error(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(new BadRequestError(ex.Message, ExceptionCode.InternalServerError.ToString()));
            }
        }
    }
}
