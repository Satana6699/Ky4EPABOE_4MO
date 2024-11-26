using MediatR;
using AutoMapper;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class CreateServiceContractCommandHandler : IRequestHandler<CreateServiceContractCommand>
{
	private readonly IServiceContractRepository _repository;
	private readonly IMapper _mapper;

	public CreateServiceContractCommandHandler(IServiceContractRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateServiceContractCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<ServiceContract>(request.ServiceContract));
		await _repository.SaveChanges();
	}
}
