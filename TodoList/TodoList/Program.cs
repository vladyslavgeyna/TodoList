using TodoList.DAL;
using TodoList.Middlewares;
using TodoList.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<XmlStorageService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseSession();
app.UseMiddleware<AddDefaultStorageCookieMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.Run();
