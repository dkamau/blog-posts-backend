using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.Core.Exceptions;
using BlogPostsBackend.Core.Interfaces;
using BlogPostsBackend.Web.ApiErrors;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogPostsBackend.Web.Endpoints.BlogPostEndpoints
{
    [Route("/BlogPosts")]
    public class Create : BaseAsyncEndpoint<BlogPostRequest, BlogPostResponse>
    {
        private readonly ICrudService<BlogPost> _blogPostService;

        public Create(ICrudService<BlogPost> blogPostService)
        {
            _blogPostService = blogPostService;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new BlogPost",
            Description = "Creates a new BlogPost",
            OperationId = "BlogPost.Create",
            Tags = new[] { "BlogPost Endpoints" })
        ]
        public override async Task<ActionResult<BlogPostResponse>> HandleAsync(BlogPostRequest request, CancellationToken cancellationToken)
        {
            try
            {
                BlogPost blogPost = await _blogPostService.CreateAsync(new BlogPost()
                {
                    UserId = request.UserId,
                    Title = request.Title,
                    Description = request.Description,
                    PublicationDate = request.PublicationDate,
                });

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
