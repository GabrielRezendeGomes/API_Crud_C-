using AutoMapper;
using CashFllow.Domain.Repositories.Expenses;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public class GetAllExpensesUseCase : IGetAllExpensesUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetAllExpensesUseCase(IExpensesReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _repository.GetAll();

        return new ResponseExpensesJson
        {
            Expense = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };


    }
}
