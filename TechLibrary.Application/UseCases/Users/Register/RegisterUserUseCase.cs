using System.ComponentModel.DataAnnotations;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Repositories;
using TechLibrary.Exception;

namespace TechLibrary.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RegisterUserUseCase(IUserRepository userRepository, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResponseRegisteredUserJson> Execute(RequestUserJson request)
    {
        Validate(request);

        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = request.Password,
        };

        await _userRepository.Add(entity);
        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = entity.Name,
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
