using CashFlow.Application.UseCases.Expenses;
using CashFlow.Communication.Enums;
using CashFlow.Exception;
using CommonTestUtilities.Requests;

namespace Validators.Teste.Expenses.Register;
public class RegisterValidatorTests
{
    [Fact]
    public void Success()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        

        //Act
        var result = validator.Validate(request);

        //Assert
        Assert.True(result.IsValid);
    }
    [Theory]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    public void Error_Title_Empty(string title)
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Title = title;   


        //Act
        var result = validator.Validate(request);

        //Assert
       
        Assert.False( result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ResourceErrorMessages.TITULO_OBRIGATORIO, result.Errors.Single().ErrorMessage);

    }

    [Fact]
    public void Error_Date_Future()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Date = DateTime.UtcNow.AddDays(1);


        //Act
        var result = validator.Validate(request);

        //Assert

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ResourceErrorMessages.GASTO_NAO_PODE_SER_FUTURO, result.Errors.Single().ErrorMessage);

    }

    [Fact]
    public void Error_Payment_Type_Invalid()
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.PaymentType = (PaymentType)60;


        //Act
        var result = validator.Validate(request);

        //Assert

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ResourceErrorMessages.METODO_PAGAMENTO_INVALIDO, result.Errors.Single().ErrorMessage);

    }
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(-3)]
    [InlineData(-7)]
    public void Error_Amoun_Invalid(decimal amount)
    {
        //Arrange
        var validator = new ExpenseValidator();
        var request = RequestRegisterExpenseJsonBuilder.Build();
        request.Amount = amount ;


        //Act
        var result = validator.Validate(request);

        //Assert

        Assert.False(result.IsValid);
        Assert.Single(result.Errors);
        Assert.Equal(ResourceErrorMessages.GASTO_TEM_QUE_SER_MAIOR_QUE_ZERO, result.Errors.Single().ErrorMessage);

    }


}
