using CashFllow.Domain.Entities;

namespace CashFllow.Domain.Repositories.Expenses;
public interface IExpensesUpdateOnlyRepository
{
    Task<Expense?> GetById(long id);
    void Update(Expense expense);
}
