using AutoMapper;
using CashFllow.Domain.Entities;
using CashFllow.Domain.Repositories.Expenses;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExeptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase : IRegisterExpenseUseCase
{
    private readonly IExpensesWriteOnlyRepositoy _repository;
    private readonly IUnityOfWork _unityOfWork;
    private readonly IMapper _mapper;
    public RegisterExpenseUseCase(IExpensesWriteOnlyRepositoy repository, IUnityOfWork unityOfWork, IMapper mapper)
    {
        _repository = repository;
        _unityOfWork = unityOfWork;
        _mapper = mapper;

    }

    public async Task<ResponseRegisterExpensesJson> Execute(RequestExpenseJson request)
    {
        Validate(request);


        var entity = _mapper.Map<Expense>(request);
        

        await _repository.Add(entity);

        await _unityOfWork.Commit();

        return _mapper.Map<ResponseRegisterExpensesJson>(entity);
    }

    private void Validate(RequestExpenseJson request)
    {
        var validator = new ExpenseValidator();
        
        var result = validator.Validate(request);

        if(result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
        
}
