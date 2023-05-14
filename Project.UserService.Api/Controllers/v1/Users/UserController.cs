using Microsoft.AspNetCore.Mvc;
using Project.UserService.Core.Entities.Requests;
using Project.UserService.Core.User.Commands;
using System.Linq;

namespace Project.UserService.Api.Controllers.v1.Users;

public class UserController : BaseController
{
    private readonly ILogger<UserController> _logger;

    /// <summary>Инициация логгера</summary>
    public UserController(ILogger<UserController> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>Создание пользователя</summary>
    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser([FromBody] UserRequest request )
    {
        var scope = new Dictionary<string, object> { { "IIN", request.IIN } };

        using (_logger.BeginScope(scope))
        {
            _logger.LogInformation("Запрошен запрос создания клиента");

            var response = await Mediator.Send(new CreateUserCommand(request));

            _logger.LogInformation("Клиент успешно создан");

            return Ok(response);
        }
    }

}
