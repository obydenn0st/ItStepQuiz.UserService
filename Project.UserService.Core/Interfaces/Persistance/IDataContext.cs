using Microsoft.EntityFrameworkCore;
using Project.UserService.Core.Entities.Persistence;

namespace Project.UserService.Core.Interfaces.Persistance;

public interface IDataContext
{
    DbSet<Users> Users { get; set; }

    int SaveChanges();

    int SaveChanges(bool acceptAllChangesOnSuccess);

    Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default);

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
