using BlogPostsBackend.Core.Interfaces;
using BlogPostsBackend.Core.Services;
using BlogPostsBackend.SharedKernel.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BlogPostsBackend.UnitTests.Core.Services.AuthenticationServiceTests.Factory
{
    internal class AuthenticationServiceFactory
    {
        internal static AuthenticationService Create(Mock<IRepository> repository, Mock<IEmailService> emailService, Mock<IConfiguration> configuration)
        {
            return new AuthenticationService(repository.Object, emailService.Object, configuration.Object);
        }
    }
}
