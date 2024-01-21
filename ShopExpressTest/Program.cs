using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using ShopExpressTest.DataAccess;
using ShopExpressTest.DataAccess.Repositories;
using ShopExpressTest.DataAccess.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

builder.Services.AddControllersWithViews();


services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
services.AddScoped(typeof(IToDoItemRepository), typeof(ToDoItemRepository));

services.AddAutoMapper(typeof(Program));

services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseInMemoryDatabase("InMemory");
});

#region Security

services.AddAntiforgery(options =>
{
    options.Cookie.Name = "AntiForgery";
});

services.Configure<CookiePolicyOptions>(options =>
{
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
    options.HttpOnly = HttpOnlyPolicy.Always;
});

#endregion

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ToDoList}/{action=Index}");

app.SeedData();

app.Run();
