using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;

public record GetSubscriberByIdQuery(Guid Id) : IRequest<SubscriberDto?>;
