using wdb_backend.DTOs;

namespace wdb_backend.Abstractions;

public interface IShowRequestsUsecase
{
    Task<List<ShowRequestsUsecaseDTO>> ExecuteAsync(Guid employerId,
        CancellationToken cancellationToken = default);
}
