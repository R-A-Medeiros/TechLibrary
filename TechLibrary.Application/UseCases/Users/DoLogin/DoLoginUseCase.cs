using AutoMapper;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Repositories;
using TechLibrary.Domain.Security.Cryptography;
using TechLibrary.Domain.Security.Tokens;
using TechLibrary.Exception;

namespace TechLibrary.Application.UseCases.Users.DoLogin;
public class DoLoginUseCase: IDoLoginUseCase
{
    private readonly IDoLoginRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPasswordEncripter _passwordEncripter;
    private readonly IAccessTokenGenerator _accessToken;

    public DoLoginUseCase(IDoLoginRepository repository
        ,IMapper mapper
        ,IPasswordEncripter passwordEncripter
        ,IAccessTokenGenerator accessToken)
    {
        _repository = repository;
        _mapper = mapper;
        _passwordEncripter = passwordEncripter;
        _accessToken = accessToken;
    }
    public async Task<ResponseRegisteredUserJson> Execute(RequestLoginJson request)
    {
        
        var entity = _mapper.Map<User>(request);

        var user = await _repository.GetUserAsync(entity);

        if (user is null) 
        {
            throw new InvalidLoginException();
        }

        var passwordIsValid = _passwordEncripter.Verify(request.Password, user);
        if(passwordIsValid == false)
        {
            throw new InvalidLoginException();
        }

        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            AccessToken = _accessToken.Generate(user)
        };

    }
}
