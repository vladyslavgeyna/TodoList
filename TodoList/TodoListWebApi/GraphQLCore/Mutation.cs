using GraphQL;
using GraphQL.Types;
using TodoList.DAL;
using TodoList.DAL.Repository;
using TodoList.DAL.Repository.Factory;
using TodoList.Service;
using TodoListWebApi.Types;

namespace TodoListWebApi.GraphQLCore
{
    public class Mutation : ObjectGraphType
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;
        public Mutation(DapperContext dapperContext,
            XmlStorageService xmlStorageService,
            CookieStorageTypeService cookieStorageTypeService)
        {
            _taskRepository = RepositoryCookieBasedFactory.GetTaskRepository(cookieStorageTypeService, dapperContext, xmlStorageService);
            _categoryRepository = RepositoryCookieBasedFactory.GetCategoryRepository(cookieStorageTypeService, dapperContext, xmlStorageService);

            Field<TaskType>("createTask")
                .Argument<NonNullGraphType<TaskInputType>>("task")
                .ResolveAsync(async context =>
                {
                    var task = context.GetArgument<TodoList.Domain.Entity.Task>("task");
                    await _taskRepository.AddAsync(task);
                    return task;
                });
        }
    }
}
