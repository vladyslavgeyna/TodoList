using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using TodoList.Enums;

namespace TodoListWebApi.Middlewares
{
    public class AddDefaultStorageSessionMiddleware
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RequestDelegate _next;
        public AddDefaultStorageSessionMiddleware(RequestDelegate next, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task InvokeAsync(HttpContext context)
        {
           // if (_httpContextAccessor?.HttpContext?.Session.GetString("Storage") is null)
          //  {
                _httpContextAccessor?.HttpContext?.Session.SetString("Storage", Storage.MsSql.ToString());
         //   }
            await _next(context);
        }
    }
}
