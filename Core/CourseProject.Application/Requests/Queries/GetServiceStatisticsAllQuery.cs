using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;
public class GetServiceStatisticsAllQuery : IRequest<IEnumerable<ServiceStatisticDto>>;