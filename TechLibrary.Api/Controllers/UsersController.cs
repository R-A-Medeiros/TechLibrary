﻿using Microsoft.AspNetCore.Mvc;
using TechLibrary.Application.UseCases.Users.Register;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterUserUseCase useCase, [FromBody]RequestUserJson request)
    {
        try
        {
            var response = await useCase.Execute(request);

            return Created(string.Empty, response);
        }
        catch(TechLibraryException ex)
        {
            return BadRequest(new ResponseErrorMessagesJson
            {
                Errors = ex.GetErrorMessages()
            });
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorMessagesJson
            {
                Errors = ["Erro Desconhecido"]
            });
        }
       
    }
}
