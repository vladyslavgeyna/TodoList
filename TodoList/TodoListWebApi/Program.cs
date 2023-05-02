using GraphQL;
using TodoList.DAL;
using TodoList.Service;
using TodoList.Service.Middleware;
using TodoListWebApi.GraphQLCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSession();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSingleton<DapperContext>();

builder.Services.AddSingleton<XmlStorageService>();

builder.Services.AddGraphQL(builder => builder
    .AddSystemTextJson()
    .AddSchema<Schema>()
    .AddGraphTypes(typeof(Schema).Assembly));

var app = builder.Build();

app.UseSession();

app.UseMiddleware<AddDefaultStorageSessionMiddleware>();

app.UseGraphQL<Schema>();

app.UseGraphQLAltair();

app.Run();
