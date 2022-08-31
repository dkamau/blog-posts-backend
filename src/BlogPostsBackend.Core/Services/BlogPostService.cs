using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BlogPostsBackend.Core.Entities.BlogPostEntities;
using BlogPostsBackend.Core.Exceptions;
using BlogPostsBackend.Core.Interfaces;
using BlogPostsBackend.Core.Models;
using BlogPostsBackend.Core.Specifications.BlogPostSpecifications;
using BlogPostsBackend.SharedKernel.Interfaces;
using Newtonsoft.Json;

namespace BlogPostsBackend.Core.Services
{
    public class BlogPostService : ICrudService<BlogPost>, IImportService
    {
        protected readonly IRepository _repository;
        protected readonly HttpClient _httpClient;

        public BlogPostService(IRepository repository)
        {
            _repository = repository;
            _httpClient = new HttpClient();
        }

        public async Task<int> CountAsync(object filter)
        {
            return await _repository.CountAsync(new GetBlogPosts((BlogPostFilter)filter));
        }

        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            if (blogPost.UserId <= 0)
                throw new CustomException(ExceptionCode.InvalidUserId);

            if (!await BlogPostExists(blogPost))
            {
                return await _repository.AddAsync(blogPost);
            }

            return null;
        }

        public Task DeleteAsync(int accountId)
        {
            throw new NotImplementedException();
        }

        public async Task<BlogPost> GetByIdAsync(int blogPostId)
        {
            return await GetBlogPost(blogPostId);
        }

        public async Task<List<BlogPost>> ListAsync(object filter)
        {
            return await _repository.ListAsync(new GetBlogPosts((BlogPostFilter)filter));
        }

        public async Task<bool> RecordExistsAsync(int blogPostId)
        {
            try
            {
                var blogPost = await GetBlogPost(blogPostId);
                return blogPost != null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private async Task<BlogPost> GetBlogPost(int? blogPostId)
        {
            if (blogPostId <= 0)
                throw new CustomException(ExceptionCode.InvalidBlogPostId);

            BlogPost blogPost = await _repository.FirstOrDefaultAsync(new GetBlogPosts(new BlogPostFilter()
            {
                Id = (int)blogPostId
            }));

            if (blogPost == null)
                throw new CustomException(ExceptionCode.BlogPostNotFound);

            return blogPost;
        }


        private async Task<bool> BlogPostExists(BlogPost blogPost)
        {
            BlogPost existingBlogPost = await _repository.FirstOrDefaultAsync(new GetBlogPosts(new BlogPostFilter()
            {
                UserId = blogPost.UserId,
                Title = blogPost.Title
            }));

            if (existingBlogPost != null)
                throw new CustomException(ExceptionCode.BlogPostAlreadyExists);

            return false;
        }

        public async Task<List<BlogPost>> ImportBlogPosts(int userId)
        {
            var response = await _httpClient.GetAsync("https://candidate-test.sq1.io/api.php");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            ImportedArticle importedArticle = JsonConvert.DeserializeObject<ImportedArticle>(responseBody);
            List<BlogPost> blogPosts = new List<BlogPost>();

            foreach (var article in importedArticle.Articles)
            {
                try
                {
                    blogPosts.Add(await CreateAsync(new BlogPost()
                    {
                        UserId = userId,
                        Title = article.Title,
                        Description = article.Description,
                        PublicationDate = article.PublishedAt
                    }));
                }
                catch(Exception) { }
            }

            return blogPosts;
        }
    }
}
