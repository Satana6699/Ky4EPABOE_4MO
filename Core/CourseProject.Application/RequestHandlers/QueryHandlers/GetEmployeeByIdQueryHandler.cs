using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDto?>
{
	private readonly IEmployeeRepository _repository;
	private readonly IMapper _mapper;

	public GetEmployeeByIdQueryHandler(IEmployeeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<EmployeeDto?> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken) => 
		_mapper.Map<EmployeeDto>(await _repository.GetById(request.Id, trackChanges: false));
}
