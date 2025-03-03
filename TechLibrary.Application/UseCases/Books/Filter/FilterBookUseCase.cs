using TechLibrary.Application.UseCases.Books.Filter;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Domain.Repositories;

namespace TechLibrary.Application.UseCases.Books.Filter;
public class FilterBookUseCase : IFilterBookUseCase
{
    private readonly IBookRepository _bookRepository;

    public FilterBookUseCase(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task<ResponseBooksJson> Execute(RequestFilterBooksJson request)
    {
        var query = await _bookRepository.Filter(request.Title, request.PageNumber);

        //transforma em metodo do repositorio
        //var totalCount = 0;
        //if (string.IsNullOrWhiteSpace(request.Title))
        //    totalCount = _dbContext.Books.Count();
        //else
        //    totalCount = _dbContext.Books.Count(Books.Title.Contains(request.Title));


        var totalCount = await _bookRepository.BookCount();

        return new ResponseBooksJson
        {
            PaginationJson = new ResponsePaginationJson
            {
                PageNumber = request.PageNumber,
                TotalCount = totalCount
            },

            Books = query.Select(book => new ResponseBookJson
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
            }).ToList()
        };
    }
}
