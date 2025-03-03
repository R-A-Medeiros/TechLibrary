using FluentValidation.Results;
using System.ComponentModel.DataAnnotations;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Repositories;
using TechLibrary.Domain.Security.Cryptography;
using TechLibrary.Domain.Security.Tokens;
using TechLibrary.Exception;

namespace TechLibrary.Application.UseCases.Users.Register;
public class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAccessTokenGenerator _accessToken;

    public RegisterUserUseCase(IUserRepository userRepository
        ,IUnitOfWork unitOfWork
        ,IPasswordEncripter passwordEncripter
        ,IAccessTokenGenerator accessToken)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _passwordEncripter = passwordEncripter;
        _accessToken = accessToken;
}

    public async Task<ResponseRegisteredUserJson> Execute(RequestUserJson request)
    {
        await Validate(request);


        var entity = new User
        {
            Email = request.Email,
            Name = request.Name,
            Password = _passwordEncripter.Encrypt(request.Password)
        };

        await _userRepository.AddAsync(entity);
        await _unitOfWork.Commit();

        return new ResponseRegisteredUserJson
        {
            Name = entity.Name,
            AccessToken = _accessToken.Generate(entity)
        };
    }

    private async Task Validate(RequestUserJson request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);
        
        var existUserWithEmail = await _userRepository.ExistsEmailAsync(request.Email);
        if (existUserWithEmail)
        {
            result.Errors.Add(new ValidationFailure("Email", "Email já registrado na plataforma"));
        }


        if (!result.IsValid)
        {
            var errorMessages = result.Errors
                .Select(error => error.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
