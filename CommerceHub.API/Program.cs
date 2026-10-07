using CommerceHub.Application.Common;
using CommerceHub.Persistence.Contexts;
using Scalar.AspNetCore;
using CommerceHub.Application;
using CommerceHub.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register Architecture Layers
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices();

// Bind the interface to the concrete EF Core implementation
builder.Services.AddScoped<ICommerceHubDbContext>(provider => provider.GetRequiredService<CommerceHubDbContext>());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
