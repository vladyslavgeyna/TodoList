using Microsoft.AspNetCore.Builder;
using TodoList.Enums;

namespace TodoList.Service.Middleware.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDefaultCookieStorage(this IApplicationBuilder app)
        {
            return app.UseMiddleware<AddDefaultStorageCookieMiddleware>();
        }
    }
}
