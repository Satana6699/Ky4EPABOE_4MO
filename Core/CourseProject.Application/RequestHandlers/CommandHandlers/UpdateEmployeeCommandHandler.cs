using MediatR;
using AutoMapper;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
{
	private readonly IEmployeeRepository _repository;
	private readonly IMapper _mapper;

	public UpdateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
	{
		var entity = await _repository.GetById(request.Employee.Id, trackChanges: true);

        if (entity is null)
        {
            return false;
        }

		_mapper.Map(request.Employee, entity);

		_repository.Update(entity);
		await _repository.SaveChanges();

		return true;
	}
}
