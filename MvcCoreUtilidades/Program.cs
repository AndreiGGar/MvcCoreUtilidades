using Microsoft.EntityFrameworkCore;
using MvcCoreUtilidades.Context;
using MvcCoreUtilidades.Helpers;
using MvcCoreUtilidades.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string connectionString = builder.Configuration.GetConnectionString("SqlHospital");
builder.Services.AddDistributedMemoryCache();
builder.Services.AddMemoryCache();
builder.Services.AddResponseCaching();
builder.Services.AddSession(options => options.IdleTimeout = TimeSpan.FromMinutes(30));
builder.Services.AddTransient<RepositoryUsuarios>();
builder.Services.AddTransient<RepositoryEmpleados>();
builder.Services.AddTransient<RepositoryCoches>();
builder.Services.AddSingleton<HelperPathProvider>();
builder.Services.AddSingleton<HelperPathImages>();
builder.Services.AddSingleton<HelperMail>();
builder.Services.AddTransient<HelperUploadFiles>();
builder.Services.AddDbContext<UsuariosContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseResponseCaching();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
