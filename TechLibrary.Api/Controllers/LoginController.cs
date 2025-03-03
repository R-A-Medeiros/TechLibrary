using Microsoft.AspNetCore.Mvc;
using TechLibrary.Application.UseCases.Users.DoLogin;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Api.Controllers;
[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> DoLogin([FromServices] IDoLoginUseCase useCase, RequestLoginJson request)
    {
        var response = await useCase.Execute(request);

        return Ok(response);
    }
}
