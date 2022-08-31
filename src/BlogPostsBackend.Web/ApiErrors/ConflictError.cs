using System.Net;

namespace BlogPostsBackend.Web.ApiErrors
{
    public class ConflictError : ApiError
    {
        public ConflictError(string message = "", string errorCode = "") :
            base(message, errorCode, (int)HttpStatusCode.Conflict, HttpStatusCode.Conflict.ToString())
        {

        }
    }
}
