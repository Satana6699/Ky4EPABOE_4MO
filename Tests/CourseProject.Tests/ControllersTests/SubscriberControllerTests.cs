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

public class SubscriberControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly SubscriberController _controller;

    public SubscriberControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new SubscriberController(_mediatorMock.Object);
    }

    
    [Fact]
    public async Task GetById_ExistingSubscriberId_ReturnsSubscriber()
    {
        // Arrange
        var subscriberId = Guid.NewGuid();
        var subscriber = new SubscriberDto { Id = subscriberId };

        _mediatorMock
            .Setup(m => m.Send(new GetSubscriberByIdQuery(subscriberId), CancellationToken.None))
            .ReturnsAsync(subscriber);

        // Act
        var result = await _controller.GetById(subscriberId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as SubscriberDto).Should().BeEquivalentTo(subscriber);

        _mediatorMock.Verify(m => m.Send(new GetSubscriberByIdQuery(subscriberId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingSubscriberId_ReturnsNotFoundResult()
    {
        // Arrange
        var subscriberId = Guid.NewGuid();
        var subscriber = new SubscriberDto { Id = subscriberId };

        _mediatorMock
            .Setup(m => m.Send(new GetSubscriberByIdQuery(subscriberId), CancellationToken.None))
            .ReturnsAsync((SubscriberDto?)null);

        // Act
        var result = await _controller.GetById(subscriberId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetSubscriberByIdQuery(subscriberId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Subscriber_ReturnsSubscriber()
    {
        // Arrange
        var subscriber = new SubscriberForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateSubscriberCommand(subscriber), CancellationToken.None));

        // Act
        var result = await _controller.Create(subscriber);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as SubscriberForCreationDto).Should().BeEquivalentTo(subscriber);

        _mediatorMock.Verify(m => m.Send(new CreateSubscriberCommand(subscriber), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateSubscriberCommand(It.IsAny<SubscriberForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingSubscriber_ReturnsNoContentResult()
    {
        // Arrange
        var subscriberId = Guid.NewGuid();
        var subscriber = new SubscriberForUpdateDto { Id = subscriberId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateSubscriberCommand(subscriber), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(subscriberId, subscriber);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateSubscriberCommand(subscriber), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingSubscriber_ReturnsNotFoundResult()
    {
        // Arrange
        var subscriberId = Guid.NewGuid();
        var subscriber = new SubscriberForUpdateDto { Id = subscriberId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateSubscriberCommand(subscriber), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(subscriberId, subscriber);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateSubscriberCommand(subscriber), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var subscriberId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(subscriberId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateSubscriberCommand(It.IsAny<SubscriberForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingSubscriberId_ReturnsNoContentResult()
    {
        // Arrange
        var subscriberId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteSubscriberCommand(subscriberId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(subscriberId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteSubscriberCommand(subscriberId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingSubscriberId_ReturnsNotFoundResult()
    {
        // Arrange
        var subscriberId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteSubscriberCommand(subscriberId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(subscriberId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteSubscriberCommand(subscriberId), CancellationToken.None), Times.Once);
    }
}

