using Domain.Interfaces.Generics;
using Domain.Interfaces.InterfaceProduct;
using Infrastructure.Configuration;
using Infrastructure.Repository.Generics;
using Infrastructure.Repository.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ApplicationApp.Interfaces;
using ApplicationApp.OpenApp;
using Domain.Interfaces.InterfaceServices;
using Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ContextBase>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ContextBase>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// INJE«’ES DE DEPENDÍNCIA

void ConfigureServices(IServiceCollection services)
{
    // INTERFACE AND REPOSITORY
    services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));
    services.AddSingleton<IProduct, RepositoryProduct>();

    // APPLICATION INTERFACE
    services.AddSingleton<IProductApp, ProductApp>();

    // DOMAIN SERVICE
    services.AddSingleton<IServiceProduct, ServiceProduct>();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
