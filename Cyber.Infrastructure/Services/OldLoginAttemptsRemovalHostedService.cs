using Cyber.Domain.Repositories;
using Microsoft.Extensions.Hosting;

namespace Cyber.Infrastructure.Services;

public class OldLoginAttemptsRemovalHostedService : IHostedService
{
    private readonly ILoginAttemptsRepository _loginAttemptsRepository;
    private readonly IConfigRepository _configRepository;
    private Timer? _timer;

    public OldLoginAttemptsRemovalHostedService(ILoginAttemptsRepository loginAttemptsRepository, IConfigRepository configRepository)
    {
        _loginAttemptsRepository = loginAttemptsRepository;
        _configRepository = configRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(RemoveOldLoginAttempts, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(60));
        return Task.CompletedTask;
    }

    private async void RemoveOldLoginAttempts(object? state)
    {
        var loginAttemptsMaxAgeInMinutes = await _configRepository.GetFailedLoginTimeoutInMinutes();
        await _loginAttemptsRepository.RemoveAttemptsOlderThan(DateTime.Now.AddMinutes(-loginAttemptsMaxAgeInMinutes));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
}