using MediatR;
using AutoMapper;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class CreateTariffPlanCommandHandler : IRequestHandler<CreateTariffPlanCommand>
{
	private readonly ITariffPlanRepository _repository;
	private readonly IMapper _mapper;

	public CreateTariffPlanCommandHandler(ITariffPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateTariffPlanCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<TariffPlan>(request.TariffPlan));
		await _repository.SaveChanges();
	}
}
