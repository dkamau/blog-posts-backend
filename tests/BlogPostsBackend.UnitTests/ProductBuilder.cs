using BlogPostsBackend.Core.Entities.BlogPostEntities;

namespace BlogPostsBackend.UnitTests
{
    // Learn more about test builders:
    // https://ardalis.com/improve-tests-with-the-builder-pattern-for-test-data
    public class BlogPostBuilder
    {
        private BlogPost _blogPost = new BlogPost();

        public BlogPostBuilder Id(int id)
        {
            _blogPost.Id = id;
            return this;
        }

        public BlogPostBuilder Title(string name)
        {
            _blogPost.Title = name;
            return this;
        }

        public BlogPostBuilder Description(string description)
        {
            _blogPost.Description = description;
            return this;
        }

        public BlogPostBuilder WithDefaultValues()
        {
            _blogPost = new BlogPost() { Id = 1, Title = "Test Item", Description = "Test Description" };

            return this;
        }

        public BlogPost Build() => _blogPost;
    }
}
