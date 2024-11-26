using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;

public record GetTariffPlanByIdQuery(Guid Id) : IRequest<TariffPlanDto?>;
