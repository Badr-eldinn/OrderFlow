using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderFlow.Application.Commands.CreateOrder;
using OrderFlow.Application.Interfaces;
using OrderFlow.Infrastructure.Caching;
using OrderFlow.Infrastructure.Data;
using OrderFlow.Infrastructure.Repositories;
using OrderFlow.Infrastructure.Services;
using StackExchange.Redis;
using OrderFlow.Infrastructure.BackgroundJobs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<OrderFlowDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddScoped<
    IDashboardQueryRepository,
    DashboardQueryRepository>();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssembly(
        typeof(CreateOrderCommand).Assembly));

builder.Services.AddSingleton<IConnectionMultiplexer>(
    ConnectionMultiplexer.Connect(
        builder.Configuration["Redis:ConnectionString"]!));
builder.Services.AddScoped<IDashboardReadModelService, DashboardReadModelService>();
builder.Services.AddHostedService<DashboardRefreshBackgroundService>();
builder.Services.AddScoped<
    IOrderProcessingService,
    OrderProcessingService>();
builder.Services.AddHostedService<OrderProcessingBackgroundService>();

builder.Services.AddScoped<ICacheService, RedisCacheService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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