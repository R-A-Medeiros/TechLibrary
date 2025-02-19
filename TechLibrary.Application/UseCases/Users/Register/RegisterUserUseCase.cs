using System.ComponentModel.DataAnnotations;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain;
using TechLibrary.Exception;

namespace TechLibrary.Application.UseCases.Users.Register;
public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestUserJson request)
    {

        Validate(request);

        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = request.Password,
        };

        //Fazer implementação dos repositorios

        return new ResponseRegisteredUserJson
        {

        };
    }

    private void Validate(RequestUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
