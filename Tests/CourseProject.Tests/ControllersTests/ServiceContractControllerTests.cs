using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using CourseProject.Application.Dtos;
using CourseProject.Application.Requests.Queries;
using CourseProject.Application.Requests.Commands;
using CourseProject.Web.Controllers;

namespace CourseProject.Tests.ControllersTests;

public class ServiceContractControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ServiceContractController _controller;

    public ServiceContractControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ServiceContractController(_mediatorMock.Object);
    }

    
    [Fact]
    public async Task GetById_ExistingServiceContractId_ReturnsServiceContract()
    {
        // Arrange
        var serviceContractId = Guid.NewGuid();
        var serviceContract = new ServiceContractDto { Id = serviceContractId };

        _mediatorMock
            .Setup(m => m.Send(new GetServiceContractByIdQuery(serviceContractId), CancellationToken.None))
            .ReturnsAsync(serviceContract);

        // Act
        var result = await _controller.GetById(serviceContractId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as ServiceContractDto).Should().BeEquivalentTo(serviceContract);

        _mediatorMock.Verify(m => m.Send(new GetServiceContractByIdQuery(serviceContractId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingServiceContractId_ReturnsNotFoundResult()
    {
        // Arrange
        var serviceContractId = Guid.NewGuid();
        var serviceContract = new ServiceContractDto { Id = serviceContractId };

        _mediatorMock
            .Setup(m => m.Send(new GetServiceContractByIdQuery(serviceContractId), CancellationToken.None))
            .ReturnsAsync((ServiceContractDto?)null);

        // Act
        var result = await _controller.GetById(serviceContractId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetServiceContractByIdQuery(serviceContractId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_ServiceContract_ReturnsServiceContract()
    {
        // Arrange
        var serviceContract = new ServiceContractForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateServiceContractCommand(serviceContract), CancellationToken.None));

        // Act
        var result = await _controller.Create(serviceContract);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as ServiceContractForCreationDto).Should().BeEquivalentTo(serviceContract);

        _mediatorMock.Verify(m => m.Send(new CreateServiceContractCommand(serviceContract), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_NullValue_ReturnsBadRequest()
    {
        // Arrange and Act
        var result = await _controller.Create(null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new CreateServiceContractCommand(It.IsAny<ServiceContractForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingServiceContract_ReturnsNoContentResult()
    {
        // Arrange
        var serviceContractId = Guid.NewGuid();
        var serviceContract = new ServiceContractForUpdateDto { Id = serviceContractId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateServiceContractCommand(serviceContract), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(serviceContractId, serviceContract);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateServiceContractCommand(serviceContract), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingServiceContract_ReturnsNotFoundResult()
    {
        // Arrange
        var serviceContractId = Guid.NewGuid();
        var serviceContract = new ServiceContractForUpdateDto { Id = serviceContractId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateServiceContractCommand(serviceContract), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(serviceContractId, serviceContract);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateServiceContractCommand(serviceContract), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var serviceContractId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(serviceContractId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateServiceContractCommand(It.IsAny<ServiceContractForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingServiceContractId_ReturnsNoContentResult()
    {
        // Arrange
        var serviceContractId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteServiceContractCommand(serviceContractId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(serviceContractId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteServiceContractCommand(serviceContractId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingServiceContractId_ReturnsNotFoundResult()
    {
        // Arrange
        var serviceContractId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteServiceContractCommand(serviceContractId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(serviceContractId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteServiceContractCommand(serviceContractId), CancellationToken.None), Times.Once);
    }
}

