using System.Security.Claims;
using Cyber.Application.Commands.AddUser;
using Cyber.Application.Commands.ChangePassword;
using Cyber.Application.DTOs.Create;
using Cyber.Application.DTOs.Read;
using Cyber.Application.DTOs.Update;
using Cyber.Application.Requests.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Guid;

namespace Cyber.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        return Ok(await _mediator.Send(new LoginUserRequest(loginUserDto.Login, loginUserDto.Password)));
    }

    [HttpPost("ChangePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var containsId = TryParse(User.Claims.First(x => x.Type == "UserId").Value, out var userId);
        if (containsId is false)
            return BadRequest();
        return Ok(await _mediator.Send(new ChangePasswordCommand(changePasswordDto.NewPassword, userId)));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Add")]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddUser([FromBody] AddUserDto addUserDto)
    {
        return Ok(await _mediator.Send(new AddUserCommand(addUserDto)));
    }
}