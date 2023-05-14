using MediatR;
using Project.UserService.Core.Entities.Requests;
using Project.UserService.Core.Entities.Responce;

namespace Project.UserService.Core.User.Commands;

public class CreateUserCommand : IRequest<UserResponce>
{
    public CreateUserCommand(UserRequest user)
    {
        User = user;
    }

    public UserRequest User { get; set; }
}
