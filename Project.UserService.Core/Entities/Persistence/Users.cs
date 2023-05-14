namespace Project.UserService.Core.Entities.Persistence;

public class Users
{
    public int Id { get; set; }
    public string IIN { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
}
