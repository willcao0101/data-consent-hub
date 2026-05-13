namespace wdb_backend.DTOs;

public class ShowRequestsUsecaseDTO
{
    public Guid RequestId { get; set; }
    public string WorkerName { get; set; }
    public List<WorkerInfoDto> InfoDescs { get; set; }
    public string Reason { get; set; }
    public DateTime Date { get; set; }
}
