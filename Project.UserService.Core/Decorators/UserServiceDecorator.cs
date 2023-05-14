using Project.UserService.Core.Interfaces.Decorators;
using Project.UserService.Core.User.Commands;

namespace Project.UserService.Core.Decorators;

public class UserServiceDecorator : IUserServiceDecorator
{
    public UserServiceDecorator()
    {
    }
    public Task<string> CreateUserCommand()
    {
        throw new NotImplementedException();
    }
}
