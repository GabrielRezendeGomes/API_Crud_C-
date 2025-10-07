using CashFllow.Domain.Enums;

namespace CashFllow.Domain.Entities;
public class Expense
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public DateTime Date { get; set; }

    public decimal Amount { get; set; }
    
    public PaymentType PaymentType { get; set; }



}
