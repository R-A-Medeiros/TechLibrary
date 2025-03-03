using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;

namespace TechLibrary.Application.UseCases.Books.Filter;
public interface IFilterBookUseCase
{
    Task<ResponseBooksJson> Execute(RequestFilterBooksJson request);
}
