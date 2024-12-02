using MediatR;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Entities;

namespace CourseProject.Application.Requests.Queries;
public class GetUsersAllQuery : IRequest<IEnumerable<UserDto>>
{
    public string? UserName { get; set; }
    public GetUsersAllQuery(string? userName)
    {
        UserName = userName;
    }
}