using GraphQL.Types;
using TodoList.DAL;
using TodoList.DAL.Repository;
using TodoList.Service;
using TodoListWebApi.Factory;
using TodoListWebApi.Types;

namespace TodoListWebApi
{
    public class Query : ObjectGraphType
    {
        private readonly ITaskRepository _taskRepository;
        public Query(IHttpContextAccessor httpContextAccessor,
            DapperContext dapperContext,
            XmlStorageService xmlStorageService)
        {
            _taskRepository = RepositoryFactory.GetTaskRepository(httpContextAccessor, dapperContext, xmlStorageService);

            Field<ListGraphType<TaskType>>("tasks")
                .ResolveAsync(async context => 
                    await _taskRepository.GetAllAsync());
        }
    }
}
