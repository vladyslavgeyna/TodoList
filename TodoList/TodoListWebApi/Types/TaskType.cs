using GraphQL.Types;

namespace TodoListWebApi.Types
{
    public class TaskType : ObjectGraphType<TodoList.Domain.Entity.Task>
    {
        public TaskType()
        {
            Description = "The task that can be created by user";
            Field(t => t.Id, nullable: false, type: typeof(IdGraphType)).Description("The unique identifier of the task.");
            Field(t => t.Description, nullable: false, type: typeof(StringGraphType)).Description("The description of the task.");
            Field(t => t.DueDate, nullable: false, type: typeof(DateTimeGraphType)).Description("The due date of the task.");
            Field(t => t.DateOfCreation, nullable: false, type: typeof(DateTimeGraphType)).Description("The date when task was created.");
            Field(t => t.IsCompleted, nullable: false, type: typeof(BooleanGraphType)).Description("Indicates if the task is completed.");
            Field(t => t.CategoryId, nullable: true, type: typeof(IntGraphType)).Description("The id of category which task belongs to (if that category exist).");
           // Field(t => t.Category, nullable: true, type: typeof(ObjectGraphType<CategoryType>)).Description("The category which task belongs to (if that category exist).");
        }
    }
}
