using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;

public record GetServiceStatisticByIdQuery(Guid Id) : IRequest<ServiceStatisticDto?>;
