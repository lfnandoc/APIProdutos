
using IntercambioGenebraAPI.Infra;
using IntercambioGenebraAPI.MapperProfiles;
using IntercambioGenebraAPI.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var serverVersion = new MySqlServerVersion(new Version(5, 7));

var connectionString = builder.Configuration.GetValue<string>("ConnectionString");
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, serverVersion));
builder.Services.AddControllers();
var assembly = AppDomain.CurrentDomain.Load("IntercambioGenebraAPI");
builder.Services.AddMediatR(assembly);

builder.Services.AddAutoMapper(cfg => 
{ 
    cfg.AddProfile<ProductViewModelProfile>(); 
}, 
assembly);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => options.EnableAnnotations());

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
