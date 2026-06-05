using Kanban.Application.UseCase.User.Delete;
using Kanban.Application.UseCase.User.Login;
using Kanban.Application.UseCase.User.Register;
using Kanban.Application.UseCase.User.Update;
using Kanban.Application.UseCase.User.UpdatePassword;
using Kanban.Communication.Requests.User;
using Kanban.Communication.Responses;
using Kanban.Communication.Responses.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Api.Controllers;

[Route(template: "[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    [HttpPost]
    [AllowAnonymous]
    [ProducesResponseType(type: typeof(RegisteredUserResponse), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase, [FromBody] RegisterUserRequest request)
    {
        RegisteredUserResponse response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }

    [HttpPost]
    [Route(template: "login")]
    [AllowAnonymous]
    [ProducesResponseType(type: typeof(LoginResponse), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromServices] ILoginUseCase useCase, [FromBody] LoginRequest request)
    {
        LoginResponse response = await useCase.Execute(request: request);
        return Ok(value: response);
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Update([FromServices] IUpdateUserUseCase useCase, [FromBody] UpdateUserRequest request)
    {
        await useCase.Execute(request: request);
        return NoContent();
    }
    
    [HttpPut(template: "password")]
    [Authorize]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdatePassword([FromServices] IUpdatePasswordUseCase useCase, [FromBody] UpdatePasswordRequest request)
    {
        await useCase.Execute(request: request);
        return NoContent();
    }

    [HttpDelete]
    [Authorize]
    [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete([FromServices] IDeleteUserUseCase useCase)
    {
        await useCase.Execute();
        return NoContent();
    }
}
