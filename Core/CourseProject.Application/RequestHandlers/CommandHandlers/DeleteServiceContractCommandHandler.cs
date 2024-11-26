using MediatR;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class DeleteServiceContractCommandHandler(IServiceContractRepository repository) : IRequestHandler<DeleteServiceContractCommand, bool>
{
	private readonly IServiceContractRepository _repository = repository;

	public async Task<bool> Handle(DeleteServiceContractCommand request, CancellationToken cancellationToken)
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
