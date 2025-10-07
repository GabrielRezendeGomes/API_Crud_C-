namespace CashFlow.Communication.Responses;
public class ResponseErrorJson
{
    public List<string> ErrorsMessages { get; set; }

    public ResponseErrorJson(string errorMessage)
    {
        ErrorsMessages = [errorMessage];
    }
    public ResponseErrorJson(List<string> errorMessage)
    {
        ErrorsMessages = errorMessage;
    }
}
