namespace CashFlow.Infrastructure.DataAccess.Repositories;

using CashFllow.Domain.Entities;
using CashFllow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

internal class ExpensesRepository : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepositoy, IExpensesUpdateOnlyRepository
{
    private readonly CashFlowDbContext _dbContext;
    public ExpensesRepository(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Expense expense)
    {
        

        await _dbContext.Expenses.AddAsync(expense);

       
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _dbContext.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (result == null)
        {
            return false;
        }

        _dbContext.Expenses.Remove(result);
         
        return true;
    }

    public async Task<List<Expense>> GetAll()
    {
       return await _dbContext.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetById(long id)
    {

        return await _dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
    }
     async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(long id)
    {

        return await _dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
    }



    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }
}
