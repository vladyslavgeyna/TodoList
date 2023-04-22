using TodoList.Data;
using TodoList.Enums;
using TodoList.Repository;
using TodoList.Services;

namespace TodoList.Utils
{
    public static class RepositorySetter
    {
        public static ITaskRepository SetTaskRepository(IHttpContextAccessor httpContextAccessor, 
            DapperContext dapperContext,
            XmlStorageService xmlStorageService)
        {
            if (httpContextAccessor?.HttpContext?.Session.GetString("Storage") == Storage.Xml.ToString())
            {
                return new TaskXmlRepository(xmlStorageService);
            }
            else if (httpContextAccessor?.HttpContext?.Session.GetString("Storage") == Storage.MsSql.ToString())
            {
                return new TaskMsSqlRepository(dapperContext);
            }
            else
            {
                throw new Exception("Storage session was not found.");
            }
        }
        public static ICategoryRepository SetCategoryRepository(IHttpContextAccessor httpContextAccessor, 
            DapperContext dapperContext,
            XmlStorageService xmlStorageService)
        {
            if (httpContextAccessor?.HttpContext?.Session.GetString("Storage") == Storage.Xml.ToString())
            {
                return new CategoryXmlRepository(xmlStorageService);
            }
            else if (httpContextAccessor?.HttpContext?.Session.GetString("Storage") == Storage.MsSql.ToString())
            {
                return new CategoryMsSqlRepository(dapperContext);
            }
            else
            {
                throw new Exception("Storage session was not found.");
            }
        }
    }
}
