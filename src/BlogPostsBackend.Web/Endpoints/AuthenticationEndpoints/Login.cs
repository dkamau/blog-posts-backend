using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using BlogPostsBackend.Core.Entities.AuthenticationEntities;
using BlogPostsBackend.Core.Exceptions;
using BlogPostsBackend.Core.Interfaces;
using BlogPostsBackend.Core.UserSecrets;
using BlogPostsBackend.Web.ApiErrors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace BlogPostsBackend.Web.Endpoints.AuthenticationEndpoints
{
    [Route("/Authentication")]
    [AllowAnonymous]
    public class Login : BaseAsyncEndpoint<LoginRequest, LoginResponse>
    {
        private readonly IAuthenticationService _authenticationService;
        protected readonly IConfiguration _configuration;

        public Login(IAuthenticationService authenticationService, IConfiguration configuration)
        {
            _authenticationService = authenticationService;
            _configuration = configuration;
        }

        [HttpPost("Login")]
        [SwaggerOperation(
            Summary = "Authenticates and logs in a user",
            Description = "Authenticates and logs in a user",
            OperationId = "Authentication.Login",
            Tags = new[] { "Authentication Endpoints" })
        ]
        public override async Task<ActionResult<LoginResponse>> HandleAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await _authenticationService.AuthenticateAsync(request.Email, request.Password);

                if (user == null)
                    return BadRequest(new BadRequestError("Invalid email or password. Please try again."));

                string token = GetToken(user);
                return Ok(LoginResponse.Create(user, token));
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

        private string GetToken(User user)
        {
            string jwtTokenSecret = Environment.GetEnvironmentVariable("BlogPosts_JWT_TOKEN_SECRET");
            if (string.IsNullOrEmpty(jwtTokenSecret))
            {
                AppSecrets appSecrets = _configuration.GetSection("AppSecrets").Get<AppSecrets>();
                if(appSecrets != null)
                    jwtTokenSecret = appSecrets.JWTTokenSecret;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(jwtTokenSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Email),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
