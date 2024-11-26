using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;

public record GetSubscribersQuery : IRequest<IEnumerable<SubscriberDto>>;
