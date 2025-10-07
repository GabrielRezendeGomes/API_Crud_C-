using AutoMapper;
using CashFllow.Domain.Repositories.Expenses;
using CashFlow.Communication.Responses;
using CashFlow.Exception;
using CashFlow.Exception.ExeptionsBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;
public class GetExpenseByIdUseCase : IGetExpenseByIdUseCase
{
    private readonly IExpensesReadOnlyRepository _repository;
    private readonly IMapper _mapper;

    public GetExpenseByIdUseCase(IExpensesReadOnlyRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseExpenseJson> Execute(long id)
    {
        var result = await _repository.GetById(id);

        if (result == null)
        {
            throw new NotFoundException(ResourceErrorMessages.DESPESA_NAO_ENCONTRADA);
        }
        
        return _mapper.Map<ResponseExpenseJson>(result);


    }
}
