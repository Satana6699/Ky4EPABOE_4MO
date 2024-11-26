using MediatR;

namespace CourseProject.Application.Requests.Commands;

public record DeleteSubscriberCommand(Guid Id) : IRequest<bool>;
