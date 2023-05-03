using Microsoft.AspNetCore.Http;
using TodoList.Enums;
using TodoList.Service.Utils;

namespace TodoList.Service
{
    public class CookieStorageTypeService
    {
        public readonly Storage Storage;
        public CookieStorageTypeService(IHttpContextAccessor httpContextAccessor,
            Storage DefaultStorage = Storage.MsSql)
        {
            var stringCookieStorageType = string.Empty;
            httpContextAccessor.HttpContext?.Request.Cookies.TryGetValue(StorageCookieHelper.CookieName, out stringCookieStorageType);
            if (stringCookieStorageType is null
                || !Enum.TryParse<Storage>(stringCookieStorageType, true, out Storage))
            {
                Storage = DefaultStorage;
            }
        }
    }
}
