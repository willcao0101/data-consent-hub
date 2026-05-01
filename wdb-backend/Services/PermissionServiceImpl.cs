using wdb_backend.Abstractions;
using wdb_backend.Models;
using wdb_backend.Common;
using wdb_backend.Data;
using wdb_backend.DTOs;

namespace wdb_backend.Services;

public class PermissionServiceImpl : IPermissionService
{
    private readonly IPermissionRepository _permissionRepo;
    private readonly IEmployerRepository _employerRepo;
    private readonly AppDbContext _context;

    public PermissionServiceImpl(IPermissionRepository permissionRepo, IEmployerRepository employerRepo, AppDbContext context)
    {
        _permissionRepo = permissionRepo;
        _employerRepo = employerRepo;
        _context = context;
    }
    public Task CreateAllByRequestAsync(Request request, IEnumerable<WorkerInfo> workerInfos, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Permission> UpdateAsync(Guid permissionId, int newStatus, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }


    public Task<IReadOnlyList<Permission>> GetAllByRequestIdAsync(Guid requestId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<Permission> GetByIdAsync(Guid permissionId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Permission>> GetAllByWorkerIdAsync(Guid workerId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<PermissionGroupDto>> GetGroupedByStatusAsync(PermissionStatus status, CancellationToken cancellationToken = default)
    {
        var permissions = await _permissionRepo.GetAllByStatusAsync(status, cancellationToken);

        var grouped = permissions.GroupBy(p => p.RequestId);

        var result = new List<PermissionGroupDto>();

        foreach (var group in grouped)
        {
            var request = await _context.Requests.FindAsync(new object[] { group.Key }, cancellationToken);
            var employer = request != null
                ? await _employerRepo.GetByIdAsync(request.EmployerId, cancellationToken)
                : null;

            result.Add(new PermissionGroupDto
            {
                RequestId = group.Key,
                EmployerName = employer?.Name ?? "Unknown",
                Permissions = group.Select(p => new PermissionItemDto
                {
                    Id = p.Id,
                    InfoId = p.InfoId,
                    WorkerId = p.WorkerId,
                    Status = (int)p.Status,
                    ExpiryDate = p.ExpiryDate,
                    LastUpdatedAt = p.LastUpdatedAt
                }).ToList()
            });
        }

        return result;
    }

}
