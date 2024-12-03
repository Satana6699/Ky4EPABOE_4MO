using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;
public class GetEmployeesAllQuery(string? Name) : IRequest<IEnumerable<EmployeeDto>>
{
    public string? Name { get; set; } = Name;
}