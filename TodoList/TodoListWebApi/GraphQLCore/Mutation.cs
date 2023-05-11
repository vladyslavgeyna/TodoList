using GraphQL;
using GraphQL.Types;
using TodoList.DAL;
using TodoList.DAL.Repository;
using TodoList.DAL.Repository.Factory;
using TodoList.Domain.Entity;
using TodoList.Enums;
using TodoList.Service;
using TodoList.Service.Utils;
using TodoListWebApi.Types;

namespace TodoListWebApi.GraphQLCore
{
    public class Mutation : ObjectGraphType
    {
        private ITaskRepository _taskRepository => RepositoryCookieBasedFactory
            .GetTaskRepository(new CookieStorageTypeService(_httpContextAccessor), 
            _context, 
            _xmlStorageService);
        private ICategoryRepository _categoryRepository => RepositoryCookieBasedFactory
            .GetCategoryRepository(new CookieStorageTypeService(_httpContextAccessor), 
            _context, 
            _xmlStorageService);

        private readonly DapperContext _context;
        private readonly XmlStorageService _xmlStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public Mutation(DapperContext dapperContext,
            XmlStorageService xmlStorageService,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = dapperContext;
            _xmlStorageService = xmlStorageService;
            _httpContextAccessor = httpContextAccessor;

            Field<TaskType>("createTask")
                .Argument<NonNullGraphType<TaskInputType>>("task")
                .ResolveAsync(async context =>
                {
                    var task = context.GetArgument<TodoList.Domain.Entity.Task>("task");
                    await _taskRepository.AddAsync(task);
                    return task;
                });

            Field<TaskType>("createCategory")
                .Argument<NonNullGraphType<CategoryInputType>>("category")
                .ResolveAsync(async context =>
                {
                    var category = context.GetArgument<Category>("category");
                    await _categoryRepository.AddAsync(category);
                    return category;
                });

            Field<StringGraphType>("changeStorageType")
                .Argument<NonNullGraphType<StorageInputType>>("storage")
                .Resolve(context =>
                {
                    var storage = context.GetArgument<Storage>("storage");
                    httpContextAccessor?.HttpContext?.Request.Cookies.TryGetValue(StorageCookieHelper.CookieName, out var a);
                    httpContextAccessor?.HttpContext?.Response.Cookies.Delete(StorageCookieHelper.CookieName);
                    httpContextAccessor?.HttpContext?.Response.Cookies.Append(StorageCookieHelper.CookieName, storage.ToString());
                    return storage.ToString();
                });
        }
    }
}
