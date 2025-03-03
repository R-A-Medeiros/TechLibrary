using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TechLibrary.Api.Services;
using TechLibrary.Application.UseCases.Checkouts;

namespace TechLibrary.Api.Controllers;
[Route("[controller]")]
[ApiController]
[Authorize]
public class CheckoutsController : ControllerBase
{
    [HttpPost]
    [Route("{bookId}")]
    public async Task<IActionResult> BookCheckout([FromServices] IRegisterBoookCheckoutUseCase useCase, Guid bookId)
    {
        var loggedUser = new LoggedUserService(HttpContext);

        await useCase.Execute(bookId);

        return NoContent();
    }
}
