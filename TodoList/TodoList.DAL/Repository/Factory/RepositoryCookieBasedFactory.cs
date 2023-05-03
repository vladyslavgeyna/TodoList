using TodoList.Enums;
using TodoList.Service;
using Microsoft.AspNetCore.Http;
using TodoList.Service.Utils;

namespace TodoList.DAL.Repository.Factory
{
    public static class RepositoryCookieBasedFactory
    {
        public static ITaskRepository GetTaskRepository(HttpContext? httpContext,
            DapperContext dapperContext,
            XmlStorageService xmlStorageService)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            var storageCookieValue = string.Empty;
            httpContext?.Request.Cookies.TryGetValue(StorageCookieHelper.CookieName, out storageCookieValue);
            if (storageCookieValue == Storage.Xml.ToString())
            {
                return new TaskXmlRepository(xmlStorageService);
            }
            else if (storageCookieValue == Storage.MsSql.ToString())
            {
                return new TaskMsSqlRepository(dapperContext);
            }
            else
            {
                throw new Exception("Storage cookie was not found.");
            }
        }
        public static ICategoryRepository GetCategoryRepository(HttpContext? httpContext,
            DapperContext dapperContext,
            XmlStorageService xmlStorageService)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }
            var storageCookieValue = string.Empty;
            httpContext?.Request.Cookies.TryGetValue(StorageCookieHelper.CookieName, out storageCookieValue);
            if (storageCookieValue == Storage.Xml.ToString())
            {
                return new CategoryXmlRepository(xmlStorageService);
            }
            else if (storageCookieValue == Storage.MsSql.ToString())
            {
                return new CategoryMsSqlRepository(dapperContext);
            }
            else
            {
                throw new Exception("Storage cookie was not found.");
            }
        }
    }
}
