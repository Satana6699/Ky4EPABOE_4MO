using MediatR;
using AutoMapper;
using CourseProject.Application.Dtos;
using CourseProject.Domain.Abstractions;
using CourseProject.Application.Requests.Queries;

namespace CourseProject.Application.RequestHandlers.QueryHandlers;

public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, PageTable<EmployeeDto>>
{
	private readonly IEmployeeRepository _repository;
	private readonly IMapper _mapper;

	public GetEmployeesQueryHandler(IEmployeeRepository repository, IMapper mapper)
	{
		_repository = repository;
		_mapper = mapper;
	}

	public async Task<PageTable<EmployeeDto>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
	{
		var totalCount = await _repository.CountAsync(request.Name);
		var empoyees = await _repository.GetPageAsync(request.Page, request.PageSize, request.Name);

		var items = _mapper.Map<IEnumerable<EmployeeDto>>(empoyees);
			return new PageTable<EmployeeDto>(items, totalCount, request.Page, request.PageSize);
	}


}
