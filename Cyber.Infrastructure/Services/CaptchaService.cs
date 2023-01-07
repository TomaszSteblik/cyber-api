using System.Net.Http.Json;
using Cyber.Application.Services;
using Cyber.Infrastructure.DTOs;
using Cyber.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Cyber.Infrastructure.Services;

public class CaptchaService : ICaptchaService
{
    private readonly IOptions<CaptchaOptions> _options;
    private readonly IHttpClientFactory _httpClientFactory;
    private const string GoogleApiEndpoint = "https://www.google.com/recaptcha/api/siteverify";
    private const string CyberCaptchaApiEndpoint = "https://cyber-captcha-5.azurewebsites.net/api/check";

    public CaptchaService(IOptions<CaptchaOptions> options, IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> VerifyReCaptchaToken(string token)
    {
        using var client = _httpClientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<GoogleRecaptchaResponseDto>($"{GoogleApiEndpoint}?secret={_options.Value.Secret}?response={token}");
        return response?.Success ?? false;
    }

    public async Task<bool> VerifyPuzzleCaptchaChallenge(string challengeId)
    {
        using var client = _httpClientFactory.CreateClient();

        var response = await client.GetFromJsonAsync<PuzzleCaptchaResponseDto>($"{GoogleApiEndpoint}?id={challengeId}");
        if (response is null)
            return false;

        var timeOfChallenge = DateTimeOffset.FromUnixTimeSeconds(response.Timestamp);
        return response.Success && timeOfChallenge.Date.AddMinutes(5) <= DateTime.UtcNow;
    }
}