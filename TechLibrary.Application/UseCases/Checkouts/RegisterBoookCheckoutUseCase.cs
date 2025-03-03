using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechLibrary.Domain.Entities;
using TechLibrary.Domain.Repositories;
using TechLibrary.Exception;

namespace TechLibrary.Application.UseCases.Checkouts;
public class RegisterBoookCheckoutUseCase : IRegisterBoookCheckoutUseCase
{
    private readonly ICheckoutRepository _checkoutRepository;
    private readonly IBookRepository _bookRepository;
    private readonly LoggedUserService _loggedUserService;

    private const int MAX_LOAN_DAYS = 7;

    public RegisterBoookCheckoutUseCase(
        ICheckoutRepository checkoutRepository
       ,IBookRepository bookRepository
       ,LoggedUserService loggedUserService)
    {
        _checkoutRepository = checkoutRepository;
        _bookRepository = bookRepository;
        _loggedUserService = loggedUserService;
    }
    public async Task Execute(Guid bookId)
    {
        await Validate(bookId);

        var user = _loggedUserService.User();

        var entity = new Checkout
        {
            UserId = user.Id,
            BookId = bookId,
            ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS),

        };

        await _checkoutRepository.AddAsync(entity);

        // está faltando a persistencia 
    }

    private async Task Validate(Guid bookId)
    {
       var book = await _bookRepository.GetByIdAsync(bookId);
       
        if(book is null)
            throw new NotFoundException("Livro não encontrado");

        var amountBookNotReturned = await _checkoutRepository
            .QuantityNotAvailableAsync(bookId);

        if(amountBookNotReturned == book.Amount)
        {
            throw new ConflitsException("Livro não está disponível para emprestimo");
        }
    }
}
