using System.Net.Http.Json;
using Cyber.Application.Services;
using Cyber.Infrastructure.DTOs;
using Cyber.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Cyber.Infrastructure.Services;

public class GoogleReCaptchaService : IReCaptchaService
{
    private readonly IOptions<CaptchaOptions> _options;
    private readonly IHttpClientFactory _httpClientFactory;
    private const string GoogleApiEndpoint = "https://www.google.com/recaptcha/api/siteverify";

    public GoogleReCaptchaService(IOptions<CaptchaOptions> options, IHttpClientFactory httpClientFactory)
    {
        _options = options;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<bool> VerifyToken(string token)
    {
        using var client = _httpClientFactory.CreateClient();
        var response = await client.GetFromJsonAsync<GoogleRecaptchaResponseDto>($"{GoogleApiEndpoint}?secret={_options.Value.Secret}?response={token}");
        return response?.Success ?? false;
    }
}