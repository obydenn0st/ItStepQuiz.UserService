namespace Project.UserService.Infrastructure.Exceptions;

public class RetryLimitException : HttpRequestException
{
    public RetryLimitException(string method, int retryAttempt)
        : base($"Выполнение {method} метода провалено после {retryAttempt} попыток")
    {
        Method = method;
    }

    public RetryLimitException(int retryAttempt)
        : base($"Выполнение метода провалено после {retryAttempt} попыток")
    {
        Method = string.Empty;
    }

    public string Method { get; }
}