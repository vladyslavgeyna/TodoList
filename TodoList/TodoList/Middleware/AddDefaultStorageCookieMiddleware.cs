using TodoList.Enums;
using TodoList.Service.Utils;

namespace TodoListMVC.Middleware
{
    public class AddDefaultStorageCookieMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly Storage _storage;

        public AddDefaultStorageCookieMiddleware(RequestDelegate next, Storage storage = Storage.MsSql)
        {
            _next = next;
            _storage = storage;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (!httpContext.Request.Cookies.ContainsKey(StorageCookieHelper.CookieName))
            {
                httpContext.Response.Cookies.Append(StorageCookieHelper.CookieName, _storage.ToString());
                httpContext.Response.Redirect("/");
            }
            else
            {
                await _next.Invoke(httpContext);
            }
        }
    }
}
