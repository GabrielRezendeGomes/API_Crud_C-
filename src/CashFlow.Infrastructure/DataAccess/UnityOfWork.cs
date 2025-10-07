using CashFllow.Domain.Repositories.Expenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Infrastructure.DataAccess;
internal class UnityOfWork : IUnityOfWork
{
    private readonly CashFlowDbContext _dbContext;
    public UnityOfWork(CashFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task Commit() => await _dbContext.SaveChangesAsync();
    
        
    
}
