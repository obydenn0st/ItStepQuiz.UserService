
using Microsoft.EntityFrameworkCore;
using Project.UserService.Core.Entities.Persistence;
using Project.UserService.Core.Interfaces.Persistance;

namespace Project.UserService.Infrastructure.Persistence;

public class DataContext : DbContext, IDataContext
{
    public DataContext() { }

    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    public DbSet<Users> Users { get; set; } = null!;
}