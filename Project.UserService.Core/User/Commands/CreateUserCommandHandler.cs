using MediatR;
using Project.UserService.Core.Entities.Responce;

namespace Project.UserService.Core.User.Commands;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, UserResponce>
{

    public async Task<UserResponce> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        throw  new NotImplementedException();
    }
}
