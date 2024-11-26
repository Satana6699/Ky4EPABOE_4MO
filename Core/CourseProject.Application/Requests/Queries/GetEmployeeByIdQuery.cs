using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;

public record GetEmployeeByIdQuery(Guid Id) : IRequest<EmployeeDto?>;
