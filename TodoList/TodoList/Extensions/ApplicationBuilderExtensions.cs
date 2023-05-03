using TodoList.Enums;
using TodoListMVC.Middleware;

namespace TodoListMVC.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDefaultCookieStorage(this IApplicationBuilder app, Storage storage = Storage.MsSql)
        {
            return app.UseMiddleware<AddDefaultStorageCookieMiddleware>(storage);
        }
    }
}
