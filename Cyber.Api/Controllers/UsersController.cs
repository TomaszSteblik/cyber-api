using Cyber.Application.Commands.AddUser;
using Cyber.Application.Commands.BlockUser;
using Cyber.Application.Commands.ChangePassword;
using Cyber.Application.Commands.ChangeUserRole;
using Cyber.Application.Commands.DeleteUser;
using Cyber.Application.Commands.GenerateOneTimePassword;
using Cyber.Application.Commands.ResetPassword;
using Cyber.Application.Commands.UpdateUser;
using Cyber.Application.Commands.UserLogout;
using Cyber.Application.DTOs.Create;
using Cyber.Application.DTOs.Delete;
using Cyber.Application.DTOs.Read;
using Cyber.Application.DTOs.Update;
using Cyber.Application.Queries.GetUsers;
using Cyber.Application.Queries.LoginUser;
using Cyber.Application.Queries.LoginUserByOneTimePassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static System.Guid;

namespace Cyber.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [AllowAnonymous]
    [HttpPost("LoginByOneTimePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> LoginUserByOneTimePassword([FromBody] LoginUserByOneTimePasswordDto loginUserDto)
    {
        return Ok(await _mediator.Send(new LoginUserByOneTimePasswordQuery(loginUserDto.Login, loginUserDto.Password)));
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<IActionResult> LoginUser([FromBody] LoginUserDto loginUserDto)
    {
        return Ok(await _mediator.Send(new LoginUserRequest(loginUserDto.Login, loginUserDto.Password)));
    }

    [HttpPost("Logout")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> LogoutUser()
    {
        var containsId = TryParse(User.Claims.First(x => x.Type == "UserId").Value, out var userId);
        if (containsId is false)
            return BadRequest();
        return Ok(await _mediator.Send(new UserLogoutCommand(userId)));
    }

    [HttpPost("ChangePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePasswordDto)
    {
        var containsId = TryParse(User.Claims.First(x => x.Type == "UserId").Value, out var userId);
        if (containsId is false)
            return BadRequest();
        return Ok(await _mediator.Send(new ChangePasswordCommand(changePasswordDto.NewPassword, userId, changePasswordDto.OldPassword)));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    public async Task<IActionResult> AddUser([FromBody] AddUserDto addUserDto)
    {
        return Ok(await _mediator.Send(new AddUserCommand(addUserDto)));
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<GetUserDto>>> GetUsers(int pageIndex = 0)
    {
        return Ok(await _mediator.Send(new GetUsersQuery(pageIndex)));
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser([FromBody] DeleteUserDto deleteUserDto)
    {
        return Ok(await _mediator.Send(new DeleteUserCommand(deleteUserDto.UserId)));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
    {
        if (updateUserDto.Password is not null && updateUserDto.OldPassword is not null)
            await _mediator.Send(new ChangePasswordCommand(
    updateUserDto.Password,
                updateUserDto.UserId,
                updateUserDto.OldPassword));
        var updatedUser = await _mediator.Send(new UpdateUserInformationsCommand(
            updateUserDto.UserId,
            updateUserDto.Username,
            updateUserDto.FirstName,
            updateUserDto.LastName,
            updateUserDto.Email));
        return Ok(updatedUser);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Block")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BlockUser([FromBody] BlockUserDto blockUserDto)
    {
        return Ok(await _mediator.Send(new BlockUserCommand(blockUserDto.UserId, true)));
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("Unlock")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UnlockUser([FromBody] BlockUserDto blockUserDto)
    {
        return Ok(await _mediator.Send(new BlockUserCommand(blockUserDto.UserId, false)));
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("Role")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ChangeRole([FromBody] ChangeUserRoleDto changeUserRole)
    {
        return Ok(await _mediator.Send(new ChangeUserRoleCommand(changeUserRole.UserId, changeUserRole.NewRole)));
    }

    [AllowAnonymous]
    [HttpPost("ResetPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ResetPassword(string email)
    {
        return Ok(await _mediator.Send(new ResetPasswordCommand(email)));
    }
    
    [Authorize(Roles = "Admin")]
    [HttpPost("GenerateOneTimePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GenerateOneTimePassword([FromQuery] Guid userId)
    {
        return Ok(await _mediator.Send(new GenerateOneTimePasswordCommand(userId)));
    }
}