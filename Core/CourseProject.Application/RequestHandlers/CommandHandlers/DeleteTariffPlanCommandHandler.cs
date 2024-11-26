using MediatR;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class DeleteTariffPlanCommandHandler(ITariffPlanRepository repository) : IRequestHandler<DeleteTariffPlanCommand, bool>
{
	private readonly ITariffPlanRepository _repository = repository;

	public async Task<bool> Handle(DeleteTariffPlanCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Id, trackChanges: false);

        if (entity is null)
        {
            return false;
        }

        _repository.Delete(entity);
        await _repository.SaveChanges();

        return true;
	}
}
