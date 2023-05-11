using GraphQL;
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
        private ITaskRepository _taskRepository => RepositoryCookieBasedFactory.
            GetTaskRepository(new CookieStorageTypeService(_httpContextAccessor), 
            _context, 
            _xmlStorageService);
        private ICategoryRepository _categoryRepository => RepositoryCookieBasedFactory.
            GetCategoryRepository(new CookieStorageTypeService(_httpContextAccessor), 
            _context, 
            _xmlStorageService);
        private readonly DapperContext _context;
        private readonly XmlStorageService _xmlStorageService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Query(DapperContext dapperContext,
            XmlStorageService xmlStorageService,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = dapperContext;
            _xmlStorageService = xmlStorageService;
            _httpContextAccessor = httpContextAccessor;

            Field<ListGraphType<TaskType>>("tasks")
                .ResolveAsync(async context => await _taskRepository.GetAllAsync());

            Field<TaskType>("task")
               .Argument<NonNullGraphType<IntGraphType>>("id")
               .ResolveAsync(async context =>
               {
                   int id = context.GetArgument<int>("id");
                   return await _taskRepository.GetByIdAsync(id);
               });

            Field<ListGraphType<CategoryType>>("categories")
               .ResolveAsync(async context =>
               {
                   return await _categoryRepository.GetAllAsync();
               });

            Field<CategoryType>("category")
               .Argument<NonNullGraphType<IntGraphType>>("id")
               .ResolveAsync(async context =>
               {
                   int id = context.GetArgument<int>("id");
                   return await _categoryRepository.GetByIdAsync(id);
               });
            _httpContextAccessor = httpContextAccessor;
        }
    }
}
