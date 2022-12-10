using Cyber.Domain.Repositories;
using Microsoft.Extensions.Hosting;

namespace Cyber.Infrastructure.Services;

public class OldLoginAttemptsRemovalHostedService : IHostedService
{
    private readonly ILoginAttemptsRepository _loginAttemptsRepository;
    private Timer? _timer;
    private const int LoginAttemptsMaxAgeInMinutes = 15;

    public OldLoginAttemptsRemovalHostedService(ILoginAttemptsRepository loginAttemptsRepository)
    {
        _loginAttemptsRepository = loginAttemptsRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(RemoveOldLoginAttempts, null, TimeSpan.Zero,
            TimeSpan.FromSeconds(60));
        return Task.CompletedTask;
    }

    private void RemoveOldLoginAttempts(object? state)
    {
        _loginAttemptsRepository.RemoveAttemptsOlderThan(DateTime.Now.AddMinutes(-LoginAttemptsMaxAgeInMinutes));
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }
}