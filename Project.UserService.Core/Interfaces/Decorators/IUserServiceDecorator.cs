namespace Project.UserService.Core.Interfaces.Decorators;

public interface IUserServiceDecorator
{
    Task<string> CreateUserCommand();
}
