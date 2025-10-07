using AutoMapper;
using CashFllow.Domain.Entities;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.AutoMapper;
internal class AutoMapping : Profile
{
    public AutoMapping()
    {
        RequestToEntity();
        EntityToRespond();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestExpenseJson, Expense>();
    }

    private void EntityToRespond()
    {
        CreateMap<Expense, ResponseRegisterExpensesJson>();
        CreateMap<Expense, ResponseShortExpenseJson>();
        CreateMap<Expense, ResponseExpenseJson>();
    }
}
