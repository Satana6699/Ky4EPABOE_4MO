using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Commands;

public record UpdateTariffPlanCommand(TariffPlanForUpdateDto TariffPlan) : IRequest<bool>;
