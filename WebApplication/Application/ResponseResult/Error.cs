namespace Application.ResponseResult;

public class Error(string errorMessage, ErrorType errorType)
{
    public string ErrorMessage { get; } = errorMessage;
    public ErrorType ErrorType { get; } = errorType;
}