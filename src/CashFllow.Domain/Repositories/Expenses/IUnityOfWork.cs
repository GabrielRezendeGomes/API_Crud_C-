namespace CashFllow.Domain.Repositories.Expenses;
public interface IUnityOfWork
{
    Task Commit();
}
