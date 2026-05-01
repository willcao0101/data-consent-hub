using wdb_backend.Models;

namespace wdb_backend.Abstractions;

public interface IEmployerRepository : IUserRepository<Employer>
{
    // get employer by id
    Task<Employer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
