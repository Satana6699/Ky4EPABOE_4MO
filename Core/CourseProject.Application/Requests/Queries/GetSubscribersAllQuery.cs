using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;
public class GetSubscribersAllQuery(string? name) : IRequest<IEnumerable<SubscriberDto>>
{
    public string? Name { get; set; } = name;
}