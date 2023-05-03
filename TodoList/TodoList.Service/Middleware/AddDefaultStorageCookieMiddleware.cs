using Microsoft.AspNetCore.Http;
using TodoList.Service.Utils;

namespace TodoList.Service.Middleware
{
    public class AddDefaultStorageCookieMiddleware
    {
        private readonly RequestDelegate _next;

        public AddDefaultStorageCookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext,
            CookieStorageTypeService cookieStorageTypeService)
        {
            if (!httpContext.Request.Cookies.ContainsKey(StorageCookieHelper.CookieName))
            {
                httpContext.Response.Cookies.Append(StorageCookieHelper.CookieName, cookieStorageTypeService.Storage.ToString());
            }
            await _next.Invoke(httpContext);
        }
    }
}
