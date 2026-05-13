using wdb_backend.Abstractions;
using wdb_backend.DTOs;
using wdb_backend.Services;

namespace wdb_backend.Usecases;

public class ShowRequestsUsecaseImpl : IShowRequestsUsecase
{
    private readonly IRequestService _requestServiceImpl;

    public ShowRequestsUsecaseImpl(IRequestService requestServiceImpl)
    {
        _requestServiceImpl = requestServiceImpl;
    }

    /// <summary>
    /// Returns a list of requests for the given employer, including each request's worker name, worker Information description and permission status.
    /// </summary>
    /// <param name="employerId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<List<ShowRequestsUsecaseDTO>> ExecuteAsync(Guid employerId,
        CancellationToken cancellationToken = default)
    {
        var requests = await _requestServiceImpl.GetAllByEmployerIdAsync(employerId);
        var ShowRequestsUsecaseDTOResult = requests.Select(r => new ShowRequestsUsecaseDTO
            {
                RequestId = r.Id,
                WorkerName = r.Worker.Name,
                InfoDescs = r.Permissions.Select(p => new WorkerInfoDto
                    { Id = p.Id, Desc = p.WorkerInfo.Desc, Status = p.Status.ToString() }).ToList(),
                Reason = r.Reason,
                Date = r.CreatedAt
            }
        ).ToList();
        return ShowRequestsUsecaseDTOResult;
    }
}
