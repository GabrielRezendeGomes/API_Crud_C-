using AutoMapper;
using CashFllow.Domain.Repositories.Expenses;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExeptionsBase;
using CashFlow.Exception;

namespace CashFlow.Application.UseCases.Expenses.DeleteById;
public class DeleteExpenseUseCase : IDeleteExpenseUseCase
{
    private readonly IUnityOfWork _unityOfWork;
    private readonly IExpensesWriteOnlyRepositoy _repository;
    public DeleteExpenseUseCase(IExpensesWriteOnlyRepositoy repository, IUnityOfWork unityOfWork)
    {
        _repository = repository;
        _unityOfWork = unityOfWork;
    }
    public async Task Execute(long id)
    {
        var result = await _repository.Delete(id);

        if (result == false)
        {
            throw new NotFoundException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
        }

        await _unityOfWork.Commit();
        


    }
}
