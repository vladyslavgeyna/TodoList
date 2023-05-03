using GraphQL.Types;

namespace TodoListWebApi.Types
{
    public class TaskInputType : InputObjectGraphType<TodoList.Domain.Entity.Task>
    {
        public TaskInputType()
        {
            Name = "TaskInput";
            Field(t => t.Description, nullable: false, type: typeof(StringGraphType));
            Field(t => t.DueDate, nullable: false, type: typeof(DateTimeGraphType));
            Field(t => t.DateOfCreation, nullable: false, type: typeof(DateTimeGraphType));
            Field(t => t.IsCompleted, nullable: false, type: typeof(BooleanGraphType));
            Field(t => t.CategoryId, nullable: false, type: typeof(IntGraphType));
        }
    }
}
