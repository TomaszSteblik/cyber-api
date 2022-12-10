using Cyber.Application.Commands.UpdateConfigAllowedLoginAttempts;
using Cyber.Application.Commands.UpdateConfigFailedLoginTimeout;
using Cyber.Application.Commands.UpdateConfigInactiveTimeout;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cyber.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class ConfigController : ControllerBase
{
    private readonly IMediator _mediator;

    public ConfigController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPut("InactiveTimeout")]
    public async Task<IActionResult> SetInactiveTimeout([FromQuery] int value)
    {
        return Ok(await _mediator.Send(new UpdateConfigInactiveTimeoutCommand(value)));
    }
    
    [HttpPut("AllowedLoginAttempts")]
    public async Task<IActionResult> SetAllowedLoginAttempts([FromQuery] int value)
    {
        return Ok(await _mediator.Send(new UpdateConfigAllowedLoginAttemptsCommand(value)));
    }
    
    [HttpPut("FailedLoginTimeout")]
    public async Task<IActionResult> SetFailedLoginTimeout([FromQuery] int value)
    {
        return Ok(await _mediator.Send(new UpdateConfigFailedLoginTimeoutCommand(value)));
    }
}