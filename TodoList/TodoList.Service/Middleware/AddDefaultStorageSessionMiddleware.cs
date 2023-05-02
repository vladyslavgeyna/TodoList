using Microsoft.AspNetCore.Http;
using TodoList.Enums;

namespace TodoList.Service.Middleware
{
    public class AddDefaultStorageSessionMiddleware
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RequestDelegate _next;
        private readonly Storage _storage;
        public AddDefaultStorageSessionMiddleware(RequestDelegate next,
            IHttpContextAccessor httpContextAccessor,
            Storage storage = Storage.MsSql)
        {
            _storage = storage;
            _next = next;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            if (_httpContextAccessor?.HttpContext?.Session.GetString("Storage") is null)
            {
                _httpContextAccessor?.HttpContext?.Session.SetString("Storage", _storage.ToString());
            }
            await _next(context);
        }
    }
}
