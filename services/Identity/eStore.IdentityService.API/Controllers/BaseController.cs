using eStore.IdentityService.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;
using System.Net;

namespace eStore.IdentityService.API.Controllers
{
        [Controller]
        public class BaseController : ControllerBase
        {
            protected string RemoteHost
            {
                get
                {
                    if (this.HttpContext.Request.Host.HasValue)
                    {
                        return this.HttpContext.Request.Host.Value;
                    }
                    return "Unknown";
                }
            }

            protected void CheckReferer(PathString path)
            {
                if (path.HasValue != path.HasValue)
                    throw new Exception();
                else if (path.HasValue == path.HasValue && !path.HasValue)
                    return;

                if (!this.HttpContext.Request.Headers.TryGetValue(HeaderNames.Referer, out StringValues refererUrl) || 1 != refererUrl.Count || null == refererUrl[0])
                    throw new Exception();

                if (!Uri.TryCreate(refererUrl[0], UriKind.Absolute, out Uri refererUri))
                    throw new Exception();

                if (
                    //host must match
                    0 != string.Compare(refererUri.Host, this.HttpContext.Request.Host.Host, StringComparison.OrdinalIgnoreCase)
                    //path must match
                    || 0 != string.Compare(path, refererUri.AbsolutePath, StringComparison.OrdinalIgnoreCase)
                    )
                {
                    throw new Exception();
                }
            }

            [NonAction]
            public StatusCodeResult StatusCode(HttpStatusCode statusCode)
            {
                return base.StatusCode((int)statusCode);
            }

            [NonAction]
            public IActionResult StatusCode<TResult>(HttpStatusCode statusCode, TResult data, bool isSuccess)
            {
                BaseResponseModel<TResult> result = new BaseResponseModel<TResult>();
                result.Result = data;
                result.Success = isSuccess;
                return base.StatusCode((int)statusCode, result);
            }

            [NonAction]
            public IActionResult ErrorCode<TResult>(HttpStatusCode statusCode, TResult data, Exception exception = null, string message = null)
            {
                BaseResponseModel<TResult> result = new BaseResponseModel<TResult>();
                result.Result = data;
                result.Exception = exception;
                result.Message = message;
                return base.StatusCode((int)statusCode, result);
            }
        }
}
