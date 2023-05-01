using GraphQL;
using TodoList.DAL;
using TodoList.Service;
using TodoListWebApi;
using TodoListWebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();

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
