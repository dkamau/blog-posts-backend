using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using BlogPostsBackend.Core.Entities.AuthenticationEntities;
using BlogPostsBackend.Core.Exceptions;
using BlogPostsBackend.Core.Interfaces;
using BlogPostsBackend.Web.ApiErrors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogPostsBackend.Web.Endpoints.AuthenticationEndpoints
{
    [Route("/Authentication")]
    [AllowAnonymous]
    public class ConfirmAccount : BaseAsyncEndpoint<string, LoginResponse>
    {
        private readonly IAuthenticationService _authenticationService;
        protected readonly IConfiguration _configuration;

        public ConfirmAccount(IAuthenticationService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        [HttpGet("ConfirmAccount/{em}")]
        [SwaggerOperation(
            Summary = "Confirms a user's account",
            Description = "Confirms a user's account",
            OperationId = "Authentication.ConfirmAccount",
            Tags = new[] { "Authentication Endpoints" })
        ]
        public override async Task<ActionResult<LoginResponse>> HandleAsync(string em, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _authenticationService.ConfirmAccountAsync(em);

                if (user == null)
                    return BadRequest(new BadRequestError("Opps! A problem was encountered while confirming your account. Please contact Blog Posts for help."));

                return Ok(LoginResponse.Create(user, ""));
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
