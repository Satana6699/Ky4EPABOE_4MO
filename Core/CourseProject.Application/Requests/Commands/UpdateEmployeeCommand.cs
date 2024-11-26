using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Commands;

public record UpdateEmployeeCommand(EmployeeForUpdateDto Employee) : IRequest<bool>;
