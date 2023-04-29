using TodoList.DAL;
using TodoList.DAL.Repository;
using TodoList.Enums;
using TodoList.Service;

namespace TodoList.Factory
{
    public static class RepositoryFactory // todo : переробити на Cookie
    {
        public static ITaskRepository GetTaskRepository(IHttpContextAccessor httpContextAccessor, 
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
        public static ICategoryRepository GetCategoryRepository(IHttpContextAccessor httpContextAccessor, 
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
