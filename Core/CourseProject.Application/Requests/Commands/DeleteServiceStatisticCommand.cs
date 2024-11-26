using MediatR;

namespace CourseProject.Application.Requests.Commands;

public record DeleteServiceStatisticCommand(Guid Id) : IRequest<bool>;
