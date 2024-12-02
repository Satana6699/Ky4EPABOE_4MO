using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;
public record class GetTariffPlansAllQuery : IRequest<IEnumerable<TariffPlanDto>>;