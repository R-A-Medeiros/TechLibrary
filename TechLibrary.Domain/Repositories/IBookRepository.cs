using TechLibrary.Domain.Entities;

namespace TechLibrary.Domain.Repositories;
public interface IBookRepository
{
    Task<List<Book>> Filter(string? title, int pageNumber);

    Task<int> BookCount();

    Task<Book> GetByIdAsync(Guid bookId);
}
