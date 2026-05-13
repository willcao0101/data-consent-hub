
using Moq;
using wdb_backend.Abstractions;
using wdb_backend.Common;
using wdb_backend.Models;
using wdb_backend.Services;
using wdb_backend.Usecases;
using wdb_backend.DTOs;

public class ShowRequestsUsecaseTests
{
    [Fact]
    public async Task ExecuteAsync_validEmployerID_ReturnListOfDTO()
    {
        // Arrange
        var mockRequestService = new Mock<IRequestService>();
        var fakeShowRequestsUsecase = new ShowRequestsUsecaseImpl(mockRequestService.Object);
        var employerId = Guid.NewGuid();
        var requests = new List<Request>();
        var workerInfo = new WorkerInfo { Id = Guid.NewGuid(), Desc = "address", Value = "havana" };
        var permission = new Permission { Id = Guid.NewGuid(), Status = PermissionStatus.Pending, InfoId = Guid.NewGuid(), WorkerInfo = workerInfo };
        var worker = new Worker { Id = Guid.NewGuid(), Name = "Iris" };
        var permissions = new List<Permission> { permission };
        var request1 = new Request { Id = Guid.NewGuid(), WorkerId = Guid.NewGuid(), Reason = "check basic", EmployerId = employerId, Permissions = permissions, WorkerInfo = workerInfo, CreatedAt = DateTime.UtcNow, Worker = worker };
        requests.Add(request1);
        mockRequestService.Setup(s => s.GetAllByEmployerIdAsync(employerId)).ReturnsAsync(requests);

        // Act
        var result = await fakeShowRequestsUsecase.ExecuteAsync(employerId);

        // Assert
        Assert.Equivalent(request1.CreatedAt, result[0].Date);
        Assert.Equal(worker.Name, result[0].WorkerName);
        Assert.Single(result[0].InfoDescs);
        Assert.Equal(request1.Reason, result[0].Reason);
    }
}
