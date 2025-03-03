using AutoMapper;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Entities;

namespace TechLibrary.Application.AutoMapper;
public class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToResponse();
    }


    private void RequestToEntity()
    {
        CreateMap<RequestLoginJson, User>();
        //CreateMap<RequestRegisterUserJson, User>()
        //    .ForMember(dest => dest.Password, config => config.Ignore());
    }

    private void EntityToResponse()
    {
        CreateMap<User, ResponseRegisteredUserJson>();
        //CreateMap<Expense, ResponseShortExpenseJson>();
        //CreateMap<Expense, ResponseExpenseJson>();
    }
}
