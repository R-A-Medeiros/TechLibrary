using Microsoft.EntityFrameworkCore;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Repositories;

namespace TechLibrary.Infra.DataAccess.Repositories;
internal class BookRepository : IBookRepository
{
    private readonly TechLibraryDbContext _context;
    private const int PAGE_SIZE = 10;
    public BookRepository(TechLibraryDbContext context)
    {
        _context = context;
    }

    public async Task<List<Book>> Filter(string? title, int pageNumber)
    {
        var skip = (pageNumber - 1) * PAGE_SIZE;

        var query = _context.Books.AsQueryable();
        if (string.IsNullOrWhiteSpace(title) == false)
        {
            query = query.Where(book => book.Title.Contains(title));
        }

        return await query.AsNoTracking()
                         .OrderBy(book => book.Title)
                         .ThenBy(book => book.Author)
                         .Skip(skip)
                         .Take(PAGE_SIZE)
                         .ToListAsync();

        


        //return await _context.Books.AsNoTracking()
        //                           .OrderBy(book => book.Title)
        //                           .ThenBy(book => book.Author)
        //                           .Skip(skip)
        //                           .Take(PAGE_SIZE)
        //                           .ToListAsync();

    }

    public async Task<int> BookCount()
    {
        return await _context.Books.CountAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid bookId)
    {
        return  await _context.Books
                              .AsNoTracking()
                              .FirstOrDefaultAsync(book => book.Id == bookId);
    }
}
