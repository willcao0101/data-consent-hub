using System.Security.Cryptography.X509Certificates;
using Moq;
using wdb_backend.Abstractions;
using wdb_backend.Models;
using wdb_backend.Services;

public class RequestServiceTests
{
    [Fact]
    public async Task CreateAsync_ValidRequest_ReturnsRequest()
    {
        //Arange
        var mockRepo = new Mock<IRequestRepository>();
        var service = new RequestServiceImpl(mockRepo.Object);
        var employerId = Guid.NewGuid();
        var workerId = Guid.NewGuid();
        var reason = "check the age";
        var fakeRequest = new Request { Id = Guid.NewGuid(), EmployerId = employerId, WorkerId = workerId, Reason = reason };
        mockRepo.Setup(r => r.AddAsync(employerId, workerId, reason, default)).ReturnsAsync(fakeRequest);

        //Act
        var result = await service.CreateAsync(employerId, workerId, reason);

        //Assert
        Assert.Equal(fakeRequest.Reason, result.Reason);

    }

    [Fact]
    public async Task CreateAsync_RepoReturnsNull_ThrowsKeyNotFoundException()
    {
        //Arange
        var mockRepo = new Mock<IRequestRepository>();
        var service = new RequestServiceImpl(mockRepo.Object);
        var reason = "check the age";

        // Act & Assert 
        await Assert.ThrowsAsync<KeyNotFoundException>(() => service.CreateAsync(Guid.NewGuid(), Guid.NewGuid(), reason));
    }

    [Fact]
    public async Task GetAllByEmployerIdAsync_validRequests_ReturnsAllMatchingRequests()
    {
        // Arrange
        var mockRepo = new Mock<IRequestRepository>();
        var service = new RequestServiceImpl(mockRepo.Object);
        var requests = new List<Request>();
        var employerId = Guid.NewGuid();
        var workerId1 = Guid.NewGuid();
        var workerId2 = Guid.NewGuid();
        var reason1 = "checkWorker1";
        var reason2 = "checkWorker2";
        var fakeRequest1 = new Request { EmployerId = employerId, WorkerId = workerId1, Reason = reason1 };
        var fakeRequest2 = new Request { EmployerId = employerId, WorkerId = workerId2, Reason = reason2 };
        requests.Add(fakeRequest1);
        requests.Add(fakeRequest2);
        mockRepo.Setup(r=>r.GetAllByEmployerIdAsync(employerId,default)).ReturnsAsync(requests);
        
        // Act
        var result = await service.GetAllByEmployerIdAsync(employerId, default);

        // Assert
        Assert.Equivalent(requests,result);
    }

    [Fact]
    public async Task  GetAllByEmployerIdAsync_NoRequests_ReturnsEmptyList()
    {
        // Arrange
        var mockRepo = new Mock<IRequestRepository>();
        var service = new RequestServiceImpl(mockRepo.Object);
        var employerId = Guid.NewGuid();
        mockRepo.Setup(r=>r.GetAllByEmployerIdAsync(employerId,default)).ReturnsAsync((List<Request>?)null);
        
        // Act
        var result = await service.GetAllByEmployerIdAsync(employerId, default);

        // Assert
        Assert.Empty(result);     
    }
}