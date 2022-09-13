using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using RepositoryLayer;
using RepositoryLayer.Repositories;
using ServiceLayer.PeopleService;
using ServiceLayer.ProductService;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("AppDB")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//adding services and repositories
builder.Services.AddScoped(typeof(PeopleRepository));
builder.Services.AddScoped(typeof(ProductRepository));
builder.Services.AddTransient<PeopleService>();
builder.Services.AddTransient<ProductService>();


var app = builder.Build();


//adding dbcontext service and calling data seed function
using var scope = app.Services.CreateScope();
var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
db.AddData().GetAwaiter().GetResult();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
