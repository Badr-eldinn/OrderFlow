using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderFlow.Application.Interfaces;

namespace OrderFlow.Infrastructure.BackgroundJobs;

public class DashboardRefreshBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public DashboardRefreshBackgroundService(
        IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();

            var dashboardService =
                scope.ServiceProvider
                    .GetRequiredService<IDashboardReadModelService>();

            await dashboardService.RefreshAsync(
                stoppingToken);

            await Task.Delay(
                TimeSpan.FromMinutes(1),
                stoppingToken);
        }
    }
}