using GraphQL.Types;
using TodoList.Enums;

namespace TodoListWebApi.Types
{
    public class StorageInputType : EnumerationGraphType<Storage>
    {
        public StorageInputType()
        {
            Name = "StorageInput";
            Description = "The type of storage to use.";
        }
    }
}
