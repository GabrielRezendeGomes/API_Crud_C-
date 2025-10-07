using System.Net;

namespace CashFlow.Exception.ExeptionsBase;
public class ErrorOnValidationException : CashFlowException
{
    private readonly List<string> _errors;

    public override int StatusCode => (int)HttpStatusCode.BadRequest; 

    public ErrorOnValidationException(List<string> errorsMessages) : base(string.Empty)
    {
        _errors = errorsMessages;
    }

    public override List<string> GetErrors()
    {
        return _errors;
    }
}
 