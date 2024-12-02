using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Queries;
public class GetServiceContractsAllQuery : IRequest<IEnumerable<ServiceContractDto>>;