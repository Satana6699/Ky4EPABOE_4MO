using MediatR;
using AutoMapper;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class UpdateTariffPlanCommandHandler : IRequestHandler<UpdateTariffPlanCommand, bool>
{
	private readonly ITariffPlanRepository _repository;
	private readonly IMapper _mapper;

	public UpdateTariffPlanCommandHandler(ITariffPlanRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateTariffPlanCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.TariffPlan.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.TariffPlan, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
