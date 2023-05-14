using Project.UserService.Core.Exceptions.Basics;

public class NotFoundException : DomainException
{
    public NotFoundException(string message) : base(message) { }
}