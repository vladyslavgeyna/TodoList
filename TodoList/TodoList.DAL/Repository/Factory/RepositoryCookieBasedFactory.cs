using TodoList.Enums;
using TodoList.Service;

namespace TodoList.DAL.Repository.Factory
{
    public static class RepositoryCookieBasedFactory
    {
        public static ITaskRepository GetTaskRepository(CookieStorageTypeService cookieStorageTypeService,
            DapperContext dapperContext,
            XmlStorageService xmlStorageService)
        {
            if (cookieStorageTypeService.Storage.ToString() == Storage.Xml.ToString())
            {
                return new TaskXmlRepository(xmlStorageService);
            }
            else if (cookieStorageTypeService.Storage.ToString() == Storage.MsSql.ToString())
            {
                return new TaskMsSqlRepository(dapperContext);
            }
            else
            {
                throw new Exception("Error while recognizing storage type.");
            }
        }
        public static ICategoryRepository GetCategoryRepository(CookieStorageTypeService cookieStorageTypeService,
            DapperContext dapperContext,
            XmlStorageService xmlStorageService)
        {
            if (cookieStorageTypeService.Storage.ToString() == Storage.Xml.ToString())
            {
                return new CategoryXmlRepository(xmlStorageService);
            }
            else if (cookieStorageTypeService.Storage.ToString() == Storage.MsSql.ToString())
            {
                return new CategoryMsSqlRepository(dapperContext);
            }
            else
            {
                throw new Exception("Error while recognizing storage type.");
            }
        }
    }
}
