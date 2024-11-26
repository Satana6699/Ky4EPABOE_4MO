using MediatR;
using AutoMapper;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class CreateServiceStatisticCommandHandler : IRequestHandler<CreateServiceStatisticCommand>
{
	private readonly IServiceStatisticRepository _repository;
	private readonly IMapper _mapper;

	public CreateServiceStatisticCommandHandler(IServiceStatisticRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateServiceStatisticCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<ServiceStatistic>(request.ServiceStatistic));
		await _repository.SaveChanges();
	}
}
