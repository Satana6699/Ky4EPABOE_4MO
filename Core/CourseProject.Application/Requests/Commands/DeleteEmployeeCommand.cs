using MediatR;

namespace CourseProject.Application.Requests.Commands;

public record DeleteEmployeeCommand(Guid Id) : IRequest<bool>;
