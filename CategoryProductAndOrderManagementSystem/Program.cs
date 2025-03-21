using CategoryProductAndOrderManagementSystem.Automapper;
using CategoryProductAndOrderManagementSystem.Data;
using CategoryProductAndOrderManagementSystem.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("AppDbConnectionString")));

builder.Services.AddScoped<ICategoriesRepository , CategoriesRepository>();
builder.Services.AddScoped<IProductsRepository , ProductsRepository>();
builder.Services.AddScoped<IOrdersRepository , OrdersRepository>();
builder.Services.AddAutoMapper(typeof(MapProfile));

var app = builder.Build();

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
