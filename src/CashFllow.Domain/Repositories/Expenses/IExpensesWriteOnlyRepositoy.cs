using CashFllow.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CashFllow.Domain.Repositories.Expenses;
public interface IExpensesWriteOnlyRepositoy
{
    Task Add(Expense expense);
    /// <summary>
    /// This function return True if The deletion was successful
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> Delete(long id);

    
    
}
