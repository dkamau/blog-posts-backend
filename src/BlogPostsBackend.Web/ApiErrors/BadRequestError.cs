using System.Net;

namespace BlogPostsBackend.Web.ApiErrors
{
    public class BadRequestError : ApiError
    {
        public BadRequestError(string message = "", string errorCode = "") :
            base(message, errorCode, (int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest.ToString())
        {

        }
    }
}
