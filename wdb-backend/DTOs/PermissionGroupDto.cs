using wdb_backend.Common;

namespace wdb_backend.DTOs;

public class PermissionItemDto
{
    public Guid Id { get; set; }
    public Guid InfoId { get; set; }
    public Guid WorkerId { get; set; }
    public int Status { get; set; }
    public DateTime ExpiryDate { get; set; }
    public DateTime LastUpdatedAt { get; set; }
}

public class PermissionGroupDto
{
    public Guid RequestId { get; set; }
    public string EmployerName { get; set; } = string.Empty;
    public List<PermissionItemDto> Permissions { get; set; } = new();
}
