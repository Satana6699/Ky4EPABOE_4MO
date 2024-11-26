using MediatR;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class DeleteSubscriberCommandHandler(ISubscriberRepository repository) : IRequestHandler<DeleteSubscriberCommand, bool>
{
	private readonly ISubscriberRepository _repository = repository;

	public async Task<bool> Handle(DeleteSubscriberCommand request, CancellationToken cancellationToken)
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
