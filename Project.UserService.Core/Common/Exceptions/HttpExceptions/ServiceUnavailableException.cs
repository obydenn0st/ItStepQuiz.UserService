namespace Project.UserService.Core.Common.Exceptions.HttpExceptions;

public abstract class ServiceUnavailableException : Exception
{
    protected ServiceUnavailableException(string message)
        : base(message)
    {
    }
}
