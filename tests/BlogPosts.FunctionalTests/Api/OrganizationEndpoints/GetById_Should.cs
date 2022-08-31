using System.Net.Http;
using System.Threading.Tasks;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.Web;
using Newtonsoft.Json;
using Xunit;

namespace BlogPostsBackend.FunctionalTests.Api.OrganizationEndpoints
{
    public class GetById_Should : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public GetById_Should(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        //[Fact]
        public async Task Include_OrganizationContact()
        {
            var response = await _client.GetAsync("BlogPosts/1");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<BlogPost>(stringResponse);

            Assert.NotNull(result);
        }
    }
}
