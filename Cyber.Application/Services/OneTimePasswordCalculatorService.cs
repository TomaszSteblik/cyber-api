using Cyber.Domain.Services;

namespace Cyber.Application.Services;

public class OneTimePasswordCalculatorService : IOneTimePasswordCalculatorService
{
    public double CalculateOneTimePassword(int x, int a)
    {
        return x / Math.Sin(a);
    }
}