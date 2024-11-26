using MediatR;
using AutoMapper;
using CourseProject.Domain.Entities;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Commands;

namespace CourseProject.Application.RequestHandlers.CommandHandlers;

public class CreateEmployeeCommandHandler : IRequestHandler<CreateEmployeeCommand>
{
	private readonly IEmployeeRepository _repository;
	private readonly IMapper _mapper;

	public CreateEmployeeCommandHandler(IEmployeeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
	{
		await _repository.Create(_mapper.Map<Employee>(request.Employee));
		await _repository.SaveChanges();
	}
}
