using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;

public record GetServiceContractByIdQuery(Guid Id) : IRequest<ServiceContractDto?>;
