namespace TodoListWebApi.GraphQLCore
{
    public class Schema : GraphQL.Types.Schema
    {
        public Schema(IServiceProvider provider) : base(provider)
        {
            Query = provider.GetRequiredService<Query>();
            Mutation = provider.GetRequiredService<Mutation>();
        }
    }
}
