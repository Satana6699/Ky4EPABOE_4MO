using MediatR;
using CourseProject.Application.Dtos;

namespace CourseProject.Application.Requests.Commands;

public record UpdateServiceContractCommand(ServiceContractForUpdateDto ServiceContract) : IRequest<bool>;
