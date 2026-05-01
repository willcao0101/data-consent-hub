using wdb_backend.Models;
using wdb_backend.Common;
using wdb_backend.DTOs;

namespace wdb_backend.Abstractions;

public interface IPermissionService
{
    Task CreateAllByRequestAsync(Request request, IEnumerable<WorkerInfo> workerInfos, CancellationToken cancellationToken = default);

    Task<Permission> UpdateAsync(Guid permissionId, int newStatus, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Permission>> GetAllByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default);

    Task<Permission> GetByIdAsync(Guid permissionId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Permission>> GetAllByWorkerIdAsync(Guid workerId, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<PermissionGroupDto>> GetGroupedByStatusAsync(PermissionStatus status, CancellationToken cancellationToken = default);

}
