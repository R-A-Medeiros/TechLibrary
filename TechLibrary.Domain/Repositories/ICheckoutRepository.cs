using TechLibrary.Domain.Entities;

namespace TechLibrary.Domain.Repositories;
public interface ICheckoutRepository
{
    Task AddAsync(Checkout checkout);
    Task<int> QuantityNotAvailableAsync(Guid bookId);
}
