using Cyber.Application.Commands.ChangePolicyStatus;
using Cyber.Application.DTOs.Update;
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
    public async Task<IActionResult> GetUserPasswordPoliciesStates(Guid userId)
    {
        return Ok(await _mediator.Send(new GetUserPasswordPoliciesQuery(userId)));
    }

    [HttpPost("Enable")]
    public async Task<IActionResult> EnablePasswordPolicy([FromBody] ChangePolicyStatusDto changePolicyStatusDto)
    {
        return Ok(await _mediator.Send(
            new ChangePolicyStatusCommand(changePolicyStatusDto.UserId, changePolicyStatusDto.Key, true)));
    }

    [HttpPost("Disable")]
    public async Task<IActionResult> DisablePasswordPolicy([FromBody] ChangePolicyStatusDto changePolicyStatusDto)
    {
        return Ok(await _mediator.Send(
            new ChangePolicyStatusCommand(changePolicyStatusDto.UserId, changePolicyStatusDto.Key, false)));
    }
}