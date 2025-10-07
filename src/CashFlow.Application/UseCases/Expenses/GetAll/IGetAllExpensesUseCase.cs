using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public interface IGetAllExpensesUseCase
{
    Task <ResponseExpensesJson> Execute();
}
