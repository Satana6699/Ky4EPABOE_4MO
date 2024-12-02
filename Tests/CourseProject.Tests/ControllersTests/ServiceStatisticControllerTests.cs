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

public class ServiceStatisticControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly ServiceStatisticController _controller;

    public ServiceStatisticControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new ServiceStatisticController(_mediatorMock.Object);
    }

    
    [Fact]
    public async Task GetById_ExistingServiceStatisticId_ReturnsServiceStatistic()
    {
        // Arrange
        var serviceStatisticId = Guid.NewGuid();
        var serviceStatistic = new ServiceStatisticDto { Id = serviceStatisticId };

        _mediatorMock
            .Setup(m => m.Send(new GetServiceStatisticByIdQuery(serviceStatisticId), CancellationToken.None))
            .ReturnsAsync(serviceStatistic);

        // Act
        var result = await _controller.GetById(serviceStatisticId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as ServiceStatisticDto).Should().BeEquivalentTo(serviceStatistic);

        _mediatorMock.Verify(m => m.Send(new GetServiceStatisticByIdQuery(serviceStatisticId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingServiceStatisticId_ReturnsNotFoundResult()
    {
        // Arrange
        var serviceStatisticId = Guid.NewGuid();
        var serviceStatistic = new ServiceStatisticDto { Id = serviceStatisticId };

        _mediatorMock
            .Setup(m => m.Send(new GetServiceStatisticByIdQuery(serviceStatisticId), CancellationToken.None))
            .ReturnsAsync((ServiceStatisticDto?)null);

        // Act
        var result = await _controller.GetById(serviceStatisticId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetServiceStatisticByIdQuery(serviceStatisticId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_ServiceStatistic_ReturnsServiceStatistic()
    {
        // Arrange
        var serviceStatistic = new ServiceStatisticForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateServiceStatisticCommand(serviceStatistic), CancellationToken.None));

        // Act
        var result = await _controller.Create(serviceStatistic);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as ServiceStatisticForCreationDto).Should().BeEquivalentTo(serviceStatistic);

        _mediatorMock.Verify(m => m.Send(new CreateServiceStatisticCommand(serviceStatistic), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateServiceStatisticCommand(It.IsAny<ServiceStatisticForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingServiceStatistic_ReturnsNoContentResult()
    {
        // Arrange
        var serviceStatisticId = Guid.NewGuid();
        var serviceStatistic = new ServiceStatisticForUpdateDto { Id = serviceStatisticId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateServiceStatisticCommand(serviceStatistic), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(serviceStatisticId, serviceStatistic);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateServiceStatisticCommand(serviceStatistic), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingServiceStatistic_ReturnsNotFoundResult()
    {
        // Arrange
        var serviceStatisticId = Guid.NewGuid();
        var serviceStatistic = new ServiceStatisticForUpdateDto { Id = serviceStatisticId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateServiceStatisticCommand(serviceStatistic), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(serviceStatisticId, serviceStatistic);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateServiceStatisticCommand(serviceStatistic), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var serviceStatisticId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(serviceStatisticId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateServiceStatisticCommand(It.IsAny<ServiceStatisticForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingServiceStatisticId_ReturnsNoContentResult()
    {
        // Arrange
        var serviceStatisticId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteServiceStatisticCommand(serviceStatisticId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(serviceStatisticId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteServiceStatisticCommand(serviceStatisticId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingServiceStatisticId_ReturnsNotFoundResult()
    {
        // Arrange
        var serviceStatisticId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteServiceStatisticCommand(serviceStatisticId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(serviceStatisticId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteServiceStatisticCommand(serviceStatisticId), CancellationToken.None), Times.Once);
    }
}

