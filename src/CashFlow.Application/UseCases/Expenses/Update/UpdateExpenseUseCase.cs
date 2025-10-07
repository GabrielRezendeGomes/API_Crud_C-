using AutoMapper;
using CashFllow.Domain.Repositories.Expenses;
using CashFlow.Communication.Requests;
using CashFlow.Exception;
using CashFlow.Exception.ExeptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update;
public class UpdateExpenseUseCase : IUpdateExpenseUseCase
{
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IExpensesUpdateOnlyRepository _repository;

    public UpdateExpenseUseCase(IMapper mapper, IUnityOfWork unityOfWork, IExpensesUpdateOnlyRepository repository)
    {
       _mapper = mapper;
        _unityOfWork = unityOfWork;
        _repository = repository;
    }

    public async Task Execute(long id, RequestExpenseJson request)
    {
        Validate(request);

        var expense = await _repository.GetById(id);
        
        if(expense is null)
        {
            throw new NotFoundException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
        }

        _mapper.Map(request, expense);

        _repository.Update(expense);
          

        await _unityOfWork.Commit();
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();

        var result = validator.Validate(request);

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
