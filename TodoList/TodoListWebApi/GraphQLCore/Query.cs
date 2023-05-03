using GraphQL.Types;
using TodoList.DAL;
using TodoList.DAL.Repository;
using TodoList.DAL.Repository.Factory;
using TodoList.Service;
using TodoListWebApi.Types;

namespace TodoListWebApi.GraphQLCore
{
    public class Query : ObjectGraphType
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;
        public Query(CookieStorageTypeService cookieStorageTypeService,
            DapperContext dapperContext,
            XmlStorageService xmlStorageService)
        {
            _taskRepository = RepositoryCookieBasedFactory.GetTaskRepository(cookieStorageTypeService, dapperContext, xmlStorageService);
            _categoryRepository = RepositoryCookieBasedFactory.GetCategoryRepository(cookieStorageTypeService, dapperContext, xmlStorageService);

            Field<ListGraphType<TaskType>>("tasks")
                .ResolveAsync(async context => await _taskRepository.GetAllAsync());



        }
    }
}
