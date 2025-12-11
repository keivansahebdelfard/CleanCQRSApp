using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyApp.API.Class;
using MyApp.Application;
using MyApp.Application.Interfaces;
using MyApp.Infrastructure.DomainEvents;
using MyApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// 🔹 اتصال به SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    )
);

builder.Services.AddAutoMapper(typeof(AssemblyMarker).Assembly);

// 🔹 Dependency Injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<DomainEventDispatcher>();


// 🔹 MediatR Registration (تمام Handlerها و Commandها را خودکار اضافه می‌کند)
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        typeof(AssemblyMarker).Assembly
    );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapAllEndpoints();


app.UseRouting();
app.UseAuthorization();

// 🔹 Map Controllers
app.MapControllers();

app.Run();
