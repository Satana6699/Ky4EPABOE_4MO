using MediatR;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class DeleteEmployeeCommandHandler(IEmployeeRepository repository) : IRequestHandler<DeleteEmployeeCommand, bool>
{
	private readonly IEmployeeRepository _repository = repository;

	public async Task<bool> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
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
