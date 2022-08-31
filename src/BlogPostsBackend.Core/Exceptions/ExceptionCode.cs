namespace BlogPostsBackend.Core.Exceptions
{
    public enum ExceptionCode
    {
        NullReference,
        InternalServerError,
        BlogPostNotFound,
        BlogPostAlreadyExists,
        InvalidBlogPostId,
        UserAlreadyExists,
        InvalidUserId,
        UserNotFound,
    }
}
