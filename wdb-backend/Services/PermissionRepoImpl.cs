using wdb_backend.Abstractions;
using wdb_backend.Models;
using Microsoft.EntityFrameworkCore;
using wdb_backend.Common;
using wdb_backend.Data;

namespace wdb_backend.Services;

public class PermissionRepoImpl : IPermissionRepository
{

    public Task AddAllByRequestAsync(Request request, LinkedList<WorkerInfo> workerInfos, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Permission> UpdateAsync(Guid permissionId, int newStatus, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<LinkedList<Permission>> GetAllByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Permission> GetOneAsync(Guid permissionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<LinkedList<Permission>> GetAllByWorkerIdAsync(Guid workerId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    private readonly AppDbContext _context;

    public PermissionRepoImpl(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IReadOnlyList<Permission>> GetAllByStatusAsync(PermissionStatus status, CancellationToken cancellationToken = default)
    {
        return await _context.Permissions
            .Where(p => p.Status == status)
            .ToListAsync(cancellationToken);
    }
}
