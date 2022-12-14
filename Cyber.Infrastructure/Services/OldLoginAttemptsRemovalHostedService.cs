using Cyber.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cyber.Infrastructure.Services;

public class OldLoginAttemptsRemovalHostedService : IHostedService
{
    private readonly IServiceProvider _services;
    private Timer? _timer;

    public OldLoginAttemptsRemovalHostedService(IServiceProvider services)
    {
        _services = services;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(RemoveOldLoginAttempts, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(60));
        return Task.CompletedTask;
    }

    private async void RemoveOldLoginAttempts(object? state)
    {
        await using var scope = _services.CreateAsyncScope();
        var scopedLoginAttemptsRepository = scope.ServiceProvider.GetRequiredService<ILoginAttemptsRepository>();
        var scopedConfigRepository = scope.ServiceProvider.GetRequiredService<IConfigRepository>();
        var loginAttemptsMaxAgeInMinutes = await scopedConfigRepository.GetFailedLoginTimeoutInMinutes();
        await scopedLoginAttemptsRepository.RemoveAttemptsOlderThan(DateTime.Now.AddMinutes(-loginAttemptsMaxAgeInMinutes));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
}