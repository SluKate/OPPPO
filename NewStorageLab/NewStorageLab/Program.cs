using DAL = NewStorageLab.DAL;
using System;
using Microsoft.EntityFrameworkCore;
using NewStorageLab.Domain.Services;
using NewStorageLab.WarehouseService;
using NewStorageLab.PoductService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IProductService, ProductService>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DAL.AppDbContext>(opt =>
{
    opt.UseSqlite(connectionString);
});

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
