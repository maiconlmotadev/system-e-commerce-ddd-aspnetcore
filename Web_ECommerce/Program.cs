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
using Entities.Entities;
using FluentAssertions.Common;
using Domain.Interfaces.UserBuyInterface;
using Domain.Interfaces.InterfaceShopping;
using Domain.Interfaces.InterfaceLogSystem;
using Google.Protobuf.WellKnownTypes;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ContextBase>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ContextBase>();
builder.Services.AddControllersWithViews();

// DEPENDENCE INJECTIONS

// INTERFACE AND REPOSITORY
builder.Services.AddSingleton(typeof(IGeneric<>), typeof(RepositoryGenerics<>));
builder.Services.AddSingleton<IProduct, RepositoryProduct>();
builder.Services.AddSingleton<IUserBuy, RepositoryUserBuy>();
builder.Services.AddSingleton<IShopping, RepositoryShopping>();
builder.Services.AddSingleton<ILogSystem, RepositoryLogSystem>();

// APPLICATION INTERFACE
builder.Services.AddSingleton<IProductApp, ProductApp>();
builder.Services.AddSingleton<IUserBuyApp, UserBuyApp>();
builder.Services.AddSingleton<IShoppingApp, ShoppingApp>();
builder.Services.AddSingleton<ILogSystemApp, LogSystemApp>();


// DOMAIN SERVICE
builder.Services.AddSingleton<IServiceProduct, ServiceProduct>();
builder.Services.AddSingleton<IServiceUserBuy, ServiceUserBuy>();



// 
//builder.Services.AddSingleton<ApplicationUser>();


var app = builder.Build();


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
