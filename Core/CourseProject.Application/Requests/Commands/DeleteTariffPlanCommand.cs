using MediatR;

namespace CourseProject.Application.Requests.Commands;

public record DeleteTariffPlanCommand(Guid Id) : IRequest<bool>;
