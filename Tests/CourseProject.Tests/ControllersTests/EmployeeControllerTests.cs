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

public class EmployeeControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly EmployeeController _controller;

    public EmployeeControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new EmployeeController(_mediatorMock.Object);
    }

    //[Fact]
    //public async Task Get_ReturnsListOfEmployees()
    //{
    //    // Arrange
    //    var employees = new List<EmployeeDto> { new(), new() };

    //    _mediatorMock
    //        .Setup(m => m.Send(new GetEmployeesQuery(), CancellationToken.None))
    //        .ReturnsAsync(employees);

    //    // Act
    //    var result = await _controller.Get();

    //    // Assert
    //    result.Should().NotBeNull();
    //    result.Should().BeOfType(typeof(OkObjectResult));

    //    var okResult = result as OkObjectResult;
    //    okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);

    //    var value = okResult?.Value as List<EmployeeDto>;
    //    value.Should().HaveCount(2);
    //    value.Should().BeEquivalentTo(employees);

    //    _mediatorMock.Verify(m => m.Send(new GetEmployeesQuery(), CancellationToken.None), Times.Once);
    //}

    [Fact]
    public async Task GetById_ExistingEmployeeId_ReturnsEmployee()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var employee = new EmployeeDto { Id = employeeId };

        _mediatorMock
            .Setup(m => m.Send(new GetEmployeeByIdQuery(employeeId), CancellationToken.None))
            .ReturnsAsync(employee);

        // Act
        var result = await _controller.GetById(employeeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(OkObjectResult));

        var okResult = result as OkObjectResult;
        okResult?.StatusCode.Should().Be((int)HttpStatusCode.OK);
        (okResult?.Value as EmployeeDto).Should().BeEquivalentTo(employee);

        _mediatorMock.Verify(m => m.Send(new GetEmployeeByIdQuery(employeeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task GetById_NotExistingEmployeeId_ReturnsNotFoundResult()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var employee = new EmployeeDto { Id = employeeId };

        _mediatorMock
            .Setup(m => m.Send(new GetEmployeeByIdQuery(employeeId), CancellationToken.None))
            .ReturnsAsync((EmployeeDto?)null);

        // Act
        var result = await _controller.GetById(employeeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new GetEmployeeByIdQuery(employeeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Create_Employee_ReturnsEmployee()
    {
        // Arrange
        var employee = new EmployeeForCreationDto();

        _mediatorMock.Setup(m => m.Send(new CreateEmployeeCommand(employee), CancellationToken.None));

        // Act
        var result = await _controller.Create(employee);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(CreatedAtActionResult));

        var createdResult = result as CreatedAtActionResult;
        createdResult?.StatusCode.Should().Be((int)HttpStatusCode.Created);
        (createdResult?.Value as EmployeeForCreationDto).Should().BeEquivalentTo(employee);

        _mediatorMock.Verify(m => m.Send(new CreateEmployeeCommand(employee), CancellationToken.None), Times.Once);
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

        _mediatorMock.Verify(m => m.Send(new CreateEmployeeCommand(It.IsAny<EmployeeForCreationDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Update_ExistingEmployee_ReturnsNoContentResult()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var employee = new EmployeeForUpdateDto { Id = employeeId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateEmployeeCommand(employee), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Update(employeeId, employee);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new UpdateEmployeeCommand(employee), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NotExistingEmployee_ReturnsNotFoundResult()
    {
        // Arrange
        var employeeId = Guid.NewGuid();
        var employee = new EmployeeForUpdateDto { Id = employeeId };

        _mediatorMock
            .Setup(m => m.Send(new UpdateEmployeeCommand(employee), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Update(employeeId, employee);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new UpdateEmployeeCommand(employee), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Update_NullValue_ReturnsBadRequest()
    {
        // Arrange
        var employeeId = Guid.NewGuid();

        // Act
        var result = await _controller.Update(employeeId, null);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(BadRequestObjectResult));
        (result as BadRequestObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);

        _mediatorMock.Verify(m => m.Send(new UpdateEmployeeCommand(It.IsAny<EmployeeForUpdateDto>()), CancellationToken.None), Times.Never);
    }

    [Fact]
    public async Task Delete_ExistingEmployeeId_ReturnsNoContentResult()
    {
        // Arrange
        var employeeId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteEmployeeCommand(employeeId), CancellationToken.None))
            .ReturnsAsync(true);

        // Act
        var result = await _controller.Delete(employeeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NoContentResult));
        (result as NoContentResult)?.StatusCode.Should().Be((int)HttpStatusCode.NoContent);

        _mediatorMock.Verify(m => m.Send(new DeleteEmployeeCommand(employeeId), CancellationToken.None), Times.Once);
    }

    [Fact]
    public async Task Delete_NotExistingEmployeeId_ReturnsNotFoundResult()
    {
        // Arrange
        var employeeId = Guid.NewGuid();

        _mediatorMock
            .Setup(m => m.Send(new DeleteEmployeeCommand(employeeId), CancellationToken.None))
            .ReturnsAsync(false);

        // Act
        var result = await _controller.Delete(employeeId);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType(typeof(NotFoundObjectResult));
        (result as NotFoundObjectResult)?.StatusCode.Should().Be((int)HttpStatusCode.NotFound);

        _mediatorMock.Verify(m => m.Send(new DeleteEmployeeCommand(employeeId), CancellationToken.None), Times.Once);
    }
}

