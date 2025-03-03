namespace TechLibrary.Application.UseCases.Checkouts;
public interface IRegisterBoookCheckoutUseCase
{
    Task Execute(Guid bookId);
}
