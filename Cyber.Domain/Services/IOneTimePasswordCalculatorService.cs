namespace Cyber.Domain.Services;

public interface IOneTimePasswordCalculatorService
{
    double CalculateOneTimePassword(int x, int a);
}