using Cyber.Application.Commands.ChangePasswordLifetime;
using Cyber.Application.Commands.ChangePolicyStatus;
using Cyber.Application.DTOs.Update;
using Cyber.Application.Queries.GetPasswordLifetime;
using Cyber.Application.Queries.GetUserPolicies;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cyber.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class PasswordPoliciesController : ControllerBase
{
    private readonly IMediator _mediator;

    public PasswordPoliciesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserPasswordPoliciesStates(Guid userId)
    {
        return Ok(await _mediator.Send(new GetUserPasswordPoliciesQuery(userId)));
    }

    [Authorize]
    [HttpGet("Me")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetLoggedUserPasswordPoliciesStates()
    {
        var containsId = Guid.TryParse(User.Claims.First(x => x.Type == "UserId").Value, out var userId);
        if (containsId is false)
            return BadRequest();
        return Ok(await _mediator.Send(new GetUserPasswordPoliciesQuery(userId)));
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
    [HttpGet("PasswordExpireTime")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPasswordLifetime(Guid userGuid)
    {
        return Ok(await _mediator.Send(new GetPasswordLifetimeQuery(userGuid)));
    }

    [Authorize(Roles = "Admin")]
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