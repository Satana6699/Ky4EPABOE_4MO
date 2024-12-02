using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;
public class GetEmployeesAllQuery : IRequest<IEnumerable<EmployeeDto>>;