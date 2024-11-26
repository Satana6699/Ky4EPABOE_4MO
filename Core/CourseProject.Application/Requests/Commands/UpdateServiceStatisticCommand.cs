using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Commands;

public record UpdateServiceStatisticCommand(ServiceStatisticForUpdateDto ServiceStatistic) : IRequest<bool>;
