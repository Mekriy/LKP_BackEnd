using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebAPI_LKP.DbContexts;
using WebAPI_LKP.Interfaces.Repositories;
using WebAPI_LKP.Interfaces.Services;
using WebAPI_LKP.Repositories;
using WebAPI_LKP.Services.RepositoryServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<WebAPI_LKP.DbContexts.AppContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("LKP_db2"));
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseHttpsRedirection();

app.MapControllers();
//DbInitializer.Seed(app);

app.Run();
