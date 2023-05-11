using GraphQL.Types;
using TodoList.Domain.Entity;

namespace TodoListWebApi.Types
{
    public class CategoryInputType : InputObjectGraphType<Category>
    {
        public CategoryInputType()
        {
            Name = "CategoryInput";
            Field(c => c.Name, nullable: false, type: typeof(StringGraphType));
        }
    }
}
