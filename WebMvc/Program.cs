using Microsoft.EntityFrameworkCore;
using RepositoryLayer;
using RepositoryLayer.Repositories;
using ServiceLayer.PeopleService;
using ServiceLayer.ProductService;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AppDB")));

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped(typeof(PeopleRepository));
builder.Services.AddScoped(typeof(ProductRepository));
builder.Services.AddTransient<PeopleService>();
builder.Services.AddTransient<ProductService>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
