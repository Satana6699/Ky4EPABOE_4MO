using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;

public record GetTariffPlansQuery : IRequest<IEnumerable<TariffPlanDto>>;
