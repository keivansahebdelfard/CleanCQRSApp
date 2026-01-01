using Microsoft.EntityFrameworkCore;
using MyApp.Application;
using MyApp.Application.Common.Interfaces;
using MyApp.Domain.Events;
using MyApp.Infrastructure.DomainEvents;
using MyApp.Infrastructure.Persistence;
using MyApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddScoped<IDomainEventContext>(sp =>
    sp.GetRequiredService<AppDbContext>());

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddApplication();

builder.Services.AddAutoMapper(typeof(AssemblyMarker).Assembly);

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
