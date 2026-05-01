using wdb_backend.Models;
using wdb_backend.Common;

namespace wdb_backend.Abstractions;

public interface IPermissionRepository
{
    // add all permissions according to request
    Task AddAllByRequestAsync(Request request, LinkedList<WorkerInfo> workerInfos, CancellationToken cancellationToken = default);

    // update status
    Task<Permission> UpdateAsync(Guid permissionId, int newStatus, CancellationToken cancellationToken = default);

    // get all permissions of specific request id
    Task<LinkedList<Permission>> GetAllByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default);

    // get one permission by id
    Task<Permission> GetOneAsync(Guid permissionId, CancellationToken cancellationToken = default);

    // get all permissions by worker id
    Task<LinkedList<Permission>> GetAllByWorkerIdAsync(Guid workerId, CancellationToken cancellationToken = default);

    // filter by status 
    Task<IReadOnlyList<Permission>> GetAllByStatusAsync(PermissionStatus status, CancellationToken cancellationToken = default);

}
