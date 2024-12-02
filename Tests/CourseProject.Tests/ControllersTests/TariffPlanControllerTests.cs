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

public class TariffPlanControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly TariffPlanController _controller;

    public TariffPlanControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new TariffPlanController(_mediatorMock.Object);
    }

    
    [Fact]
    public async Task GetById_ExistingTariffPlanId_ReturnsTariffPlan()
    {
        // Arrange
        var tariffPlanId = Guid.NewGuid();
        var tariffPlan = new TariffPlanDto { Id = tariffPlanId };

        _mediatorMock
            .Setup(m => m.Send(new GetTariffPlanByIdQuery(tariffPlanId), CancellationToken.None))
            .ReturnsAsync(tariffPlan);

        // Act
        var result = await _controller.GetById(tariffPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as TariffPlanDto).Should().BeEquivalentTo(tariffPlan);

        _mediatorMock.Verify(m => m.Send(new GetTariffPlanByIdQuery(tariffPlanId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingTariffPlanId_ReturnsNotFoundResult()
    {
        // Arrange
        var tariffPlanId = Guid.NewGuid();
        var tariffPlan = new TariffPlanDto { Id = tariffPlanId };

        _mediatorMock
            .Setup(m => m.Send(new GetTariffPlanByIdQuery(tariffPlanId), CancellationToken.None))
            .ReturnsAsync((TariffPlanDto?)null);

        // Act
        var result = await _controller.GetById(tariffPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetTariffPlanByIdQuery(tariffPlanId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_TariffPlan_ReturnsTariffPlan()
    {
        // Arrange
        var tariffPlan = new TariffPlanForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateTariffPlanCommand(tariffPlan), CancellationToken.None));

        // Act
        var result = await _controller.Create(tariffPlan);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as TariffPlanForCreationDto).Should().BeEquivalentTo(tariffPlan);

        _mediatorMock.Verify(m => m.Send(new CreateTariffPlanCommand(tariffPlan), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateTariffPlanCommand(It.IsAny<TariffPlanForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingTariffPlan_ReturnsNoContentResult()
    {
        // Arrange
        var tariffPlanId = Guid.NewGuid();
        var tariffPlan = new TariffPlanForUpdateDto { Id = tariffPlanId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateTariffPlanCommand(tariffPlan), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(tariffPlanId, tariffPlan);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateTariffPlanCommand(tariffPlan), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingTariffPlan_ReturnsNotFoundResult()
    {
        // Arrange
        var tariffPlanId = Guid.NewGuid();
        var tariffPlan = new TariffPlanForUpdateDto { Id = tariffPlanId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateTariffPlanCommand(tariffPlan), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(tariffPlanId, tariffPlan);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateTariffPlanCommand(tariffPlan), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var tariffPlanId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(tariffPlanId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateTariffPlanCommand(It.IsAny<TariffPlanForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingTariffPlanId_ReturnsNoContentResult()
    {
        // Arrange
        var tariffPlanId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteTariffPlanCommand(tariffPlanId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(tariffPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteTariffPlanCommand(tariffPlanId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingTariffPlanId_ReturnsNotFoundResult()
    {
        // Arrange
        var tariffPlanId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteTariffPlanCommand(tariffPlanId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(tariffPlanId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteTariffPlanCommand(tariffPlanId), CancellationToken.None), Times.Once);
    }
}

