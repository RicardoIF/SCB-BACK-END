
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Contracts.Interfaces
{
    public interface IRepositoryContext : IAuditDbContext, IDisposable
    {

        DatabaseFacade Database { get; }
        int SaveChange();
        Task<int> SaveChangesAsync();
    }
}
