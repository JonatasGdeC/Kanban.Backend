using Kanban.Application.UseCase.Board.Register;
using Kanban.Communication.Dtos;
using Kanban.Communication.Requests.Board;
using Kanban.Communication.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kanban.Api.Controllers;

[Route(template: "[controller]")]
[ApiController]
[Authorize]
public class BoardController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(type: typeof(BoardDto), statusCode: StatusCodes.Status201Created)]
    [ProducesResponseType(type: typeof(ErrorResponse), statusCode: StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterBoardUseCase useCase, [FromBody] RegisterBoardRequest request)
    {
        BoardDto response = await useCase.Execute(request: request);
        return Created(uri: string.Empty, value: response);
    }
}