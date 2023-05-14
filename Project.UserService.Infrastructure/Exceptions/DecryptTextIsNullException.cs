using Project.UserService.Core.Exceptions.Basics;

namespace Project.UserService.Infrastructure.Exceptions;

/// <summary>Описывает проблему при расшифровке текста со значением NULL</summary>
public class DecryptTextIsNullException : DomainException
{
    /// <inheritdoc />
    public DecryptTextIsNullException() : base("Текст для расшифровки не может иметь значение NULL") { }
}