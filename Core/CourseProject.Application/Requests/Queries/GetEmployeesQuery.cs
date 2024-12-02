using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;

public record GetEmployeesQuery : IRequest<PageTable<EmployeeDto>>
{
    public int Page {  get; set; }
    public int PageSize { get; set; }
    public string? Name { get; set; }

    public GetEmployeesQuery(int page, int pageSize, string? name)
    {
        Page = page;
        PageSize = pageSize; 
        Name = name;
    }
}
