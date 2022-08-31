using System.Threading.Tasks;

namespace BlogPostsBackend.Core.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}
