using Cyber.Application.Commands.ChangePasswordLifetime;
using Cyber.Application.Commands.ChangePolicyStatus;
using Cyber.Application.DTOs.Update;
using Cyber.Application.Queries.GetPasswordLifetime;
using Cyber.Application.Queries.GetUserPolicies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cyber.Api.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("[controller]")]
public class PasswordPoliciesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PasswordPoliciesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserPasswordPoliciesStates(Guid userId)
    {
        return Ok(await _mediator.Send(new GetUserPasswordPoliciesQuery(userId)));
    }

    [HttpPost("Enable")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> EnablePasswordPolicy([FromBody] ChangePolicyStatusDto changePolicyStatusDto)
    {
        return Ok(await _mediator.Send(
            new ChangePolicyStatusCommand(changePolicyStatusDto.UserId, changePolicyStatusDto.Key, true)));
    }

    [HttpPost("Disable")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DisablePasswordPolicy([FromBody] ChangePolicyStatusDto changePolicyStatusDto)
    {
        return Ok(await _mediator.Send(
            new ChangePolicyStatusCommand(changePolicyStatusDto.UserId, changePolicyStatusDto.Key, false)));
    }

    [HttpGet("PasswordExpireTime")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPasswordLifetime(Guid userGuid)
    {
        return Ok(await _mediator.Send(new GetPasswordLifetimeQuery(userGuid)));
    }

    [HttpPost("PasswordExpireTime")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangePasswordLifetime([FromBody] ChangePasswordLifetimeDto changePasswordLifetimeDto)
    {
        return Ok(await _mediator.Send(new ChangePasswordLifetimeCommand(changePasswordLifetimeDto.UserId, changePasswordLifetimeDto.ExpireTimeInDays)));
    }

}