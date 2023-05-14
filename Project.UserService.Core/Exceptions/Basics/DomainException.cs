namespace Project.UserService.Core.Exceptions.Basics;

public abstract class DomainException : Exception
{
    /// <summary>
    /// Initialize <see cref="DomainException"/> instance
    /// </summary>
    /// <param name="message"></param>
    public DomainException(string message) : base(message) { }
}
