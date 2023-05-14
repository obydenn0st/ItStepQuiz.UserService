namespace Project.UserService.Core.Common.Exceptions.HttpExceptions;

public sealed class RetryLimitException : ServiceUnavailableException
{
    public RetryLimitException(string method)
        : base("Превышен лимит запросов в метод " + method)
    {
    }
}
