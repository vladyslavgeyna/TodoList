using GraphQL.Types;

namespace TodoListWebApi.Types
{
    public class CategoryType : ObjectGraphType<TodoList.Domain.Entity.Category>
    {
        public CategoryType()
        {
            Description = "The category that can be created by user to make task sorting easier.";
            Field(c => c.Id, nullable: false, type: typeof(IdGraphType)).Description("The unique identifier of the category.");
            Field(c => c.Name, nullable: false, type: typeof(StringGraphType)).Description("The name of this category.");
           // Field(c => c.Tasks, nullable: false, type: typeof(ListGraphType<TaskType>)).Description("The tasks of this category.");
        }
    }
}
