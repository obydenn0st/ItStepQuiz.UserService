using Project.UserService.Core.Exceptions.Basics;

namespace Project.UserService.Infrastructure.Exceptions;

/// <summary>Описывает проблему во время расшифровки</summary>
public class DecryptException : DomainException
{
    /// <inheritdoc />
    public DecryptException(string text, string secretKey)
        : base($"Ошибка во время расшифровки строки {text} с помощью ключа {secretKey}") { }
}