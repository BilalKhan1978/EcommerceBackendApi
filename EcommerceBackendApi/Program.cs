using AutoMapper;
using EcommerceBackendApi.Data;
using EcommerceBackendApi.Services.Implementations;
using EcommerceBackendApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpClient();

// Add services to the container.

builder.Services.AddControllers();



// Add dependencies for the following services 
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStoreService, StoreService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// add dependency to connect with Sql Server
builder.Services.AddDbContext<EcommerceDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("EcommerceApiConnectionString")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
