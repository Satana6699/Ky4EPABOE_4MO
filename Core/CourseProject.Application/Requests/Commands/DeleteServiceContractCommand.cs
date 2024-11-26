using MediatR;

namespace CourseProject.Application.Requests.Commands;

public record DeleteServiceContractCommand(Guid Id) : IRequest<bool>;
