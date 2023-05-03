using GraphQL;
using TodoList.DAL;
using TodoList.Service;
using TodoListWebApi.GraphQLCore;
using TodoList.Service.Middleware.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<CookieStorageTypeService>();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<XmlStorageService>();
builder.Services.AddGraphQL(builder => builder
    .AddSystemTextJson()
    .AddSchema<Schema>()
    .AddGraphTypes(typeof(Schema).Assembly));

var app = builder.Build();

app.UseDefaultCookieStorage();
app.UseGraphQL<Schema>();
app.UseGraphQLAltair();
app.Run();
