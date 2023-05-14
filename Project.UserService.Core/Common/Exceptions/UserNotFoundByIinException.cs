namespace Project.UserService.Core.Common.Exceptions;

public class UserNotFoundByIinException : NotFoundException
{
    public UserNotFoundByIinException(string iin) : base($"Пользователь с ИИН {iin} не найден") { }
}