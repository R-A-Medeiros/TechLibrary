using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Repositories;

namespace TechLibrary.Infra.DataAccess.Repositories;
internal class CheckoutRepository : ICheckoutRepository
{
    private readonly TechLibraryDbContext _context;
    public CheckoutRepository(TechLibraryDbContext context)
    {
        _context = context;
    }
    public Task AddAsync(Checkout checkout)
    {
        throw new NotImplementedException();
    }

    public async Task<int> QuantityNotAvailableAsync(Guid bookId)
    {
        return await _context
               .Checkouts
               .CountAsync(checkout => checkout.BookId == bookId && checkout.ReturnedDate == null);
    }
}
