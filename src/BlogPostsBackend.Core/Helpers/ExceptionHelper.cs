using System;
using BlogPostsBackend.Core.Exceptions;

namespace BlogPostsBackend.Core.Helpers
{
    public static class ExceptionHelper
    {
        internal static string GetExceptionMessage(ExceptionCode exceptionCode)
        {
            switch (exceptionCode)
            {
                case ExceptionCode.InternalServerError:
                    return "An error occured while processing your request";
                case ExceptionCode.NullReference:
                    return "Value should not be null";
                case ExceptionCode.InvalidUserId:
                    return "Invalid user id";
                case ExceptionCode.UserAlreadyExists:
                    return "User already exists";
                case ExceptionCode.UserNotFound:
                    return "User not found";
                case ExceptionCode.InvalidBlogPostId:
                    return "Invalid blogPost id";
                case ExceptionCode.BlogPostAlreadyExists:
                    return "BlogPost already exists";
                case ExceptionCode.BlogPostNotFound:
                    return "BlogPost not found";
                default:
                    return "Invalid request";
                    
            }
        }

        internal static string GetNullExceptionMessage(string value)
        {
            return $"{value} should not be null.";
        }
    }
}
